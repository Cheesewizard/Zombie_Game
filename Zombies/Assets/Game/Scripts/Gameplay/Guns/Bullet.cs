using System;
using Assets.Scripts.Interfaces;
using Game.Configs;
using Game.Scripts.Gameplay.Services;
using Game.Scripts.Gameplay.WorldObjects;
using Game.Scripts.Utils;
using Game.Utils;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class Bullet : ResetableObject, IDamageDealer
	{
		[Inject]
		private BulletImpactService bulletImpactService = null;

		[SerializeField, Required, Find(Destination.Self)]
		private BulletVisualiser visualiser;

		[SerializeField]
		private float maxPenetrationPower = 100f;

		public ParticleSystem BloodHitEffect => bloodHitEffect;

		public float ImpactForce => impactForce;
		public Ray Ray => ray;

		private Ray ray;
		private float impactForce;
		private float bulletSpeed;
		private ParticleSystem bloodHitEffect;
		private float effectiveRange;
		private bool isFlying;
		private float penetrationPower;
		private float bonusDamageMultiplier;
		private Gun firingGun;
		public GunConfig GunConfig => firingGun.GunConfig;

		public float Damage => firingGun.GunConfig.Stats.Damage * (penetrationPower / maxPenetrationPower) * (1.0f + bonusDamageMultiplier);
		public float CriticalDamagePercentage => firingGun.GunConfig.Stats.CriticalDamagePercentage;
		public float CriticalChancePercentage => firingGun.GunConfig.Stats.CriticalChancePercentage;

		[SerializeField, ValueDropdown(OdinDropdowns.WEAPONS)]
		private int gunSourceID;

		public override void Init()
		{
			base.Init();
			visualiser.InitVisual();
			penetrationPower = maxPenetrationPower;
		}

		public void ResetTransform(Transform parent)
		{
			base.ResetTransform(parent);

			isFlying = false;
			visualiser.InitVisual();

			penetrationPower = maxPenetrationPower;
		}

		private void StartFlying()
		{
			isFlying = true;
			visualiser.StartDrawLine(transform.position);
		}

		private void StopFlying(bool hit)
		{
			isFlying = false;
			visualiser.StopDrawLine(hit);
			//onStopFlying?.Invoke(hit);was
		}

		private void StopFlying(RaycastHit hit)
		{
			transform.position = hit.point;
			visualiser.UpdateLinePosition(hit.point);
			StopFlying(true);
		}

		public void Launch(Gun gun, Action<bool> onStopFlying = null)
		{
			firingGun = gun;

			ray.origin = transform.position;
			ray.direction = -transform.up;
			impactForce = gun.GunConfig.ImpactForce;
			effectiveRange = gun.GunConfig.EffectiveRange;
			bulletSpeed = gun.GunConfig.BulletSpeed.Random();
			bloodHitEffect = gun.GunConfig.BloodHitFeedbackPrefab;

			// Detach the bullet from the parent (magazine)
			transform.DetatchFromParent();

			StartFlying();
		}

		private void Update()
		{
			if (!isFlying) return;

			var distance = bulletSpeed * Time.deltaTime;
			RaycastHit2D[] hits =  Physics2D.RaycastAll(ray.origin, ray.direction, distance, LayerMasks.BulletLayer);
			Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
			foreach (var hit in hits)
			{
				var target = hit.collider.GetComponent<BulletTarget>();
				if (target == null)
				{
					target = (hit.rigidbody != null) ? hit.rigidbody.GetComponent<BulletTarget>() : null;
				}

				// Check if the bullet hit a collider with a collider material provider. If so play a particle and sound.
				if (hit.collider.TryGetComponent<ColliderMaterialProvider>(out var colliderMaterialProvider))
				{
					var material = colliderMaterialProvider.SurfaceMaterial;

					//Play the sound for the material the bullet hit if there is a sound for it.
					bulletImpactService.PlaySound(gunSourceID, material);

					//Play the particle for the material the bullet hit if there is a particle system for it.
					var rotation = Quaternion.LookRotation(hit.normal);

					var shouldParent = colliderMaterialProvider.ShouldAttachImpactVfx;

					bulletImpactService.PlayParticle(hit.collider.transform, hit.point, rotation, gunSourceID, material, shouldParent);
					// bulletImpactService.LeaveHitDecal(hit.collider.transform, hit.point, rotation, gunSourceID, material, shouldParent);
				}

				Debug.Log("Object Hit" + target?.name);
				if (target != null)
				{
					target.NotifyBulletHit(this, hit);
					if (target.IsImpenetrable)
					{
						StopFlying(hit);
						visualiser.HideBullet();
						return;
					}
				}
				// No target? Stop the bullet -- as you might just hit a default collider (like floors and walls).
				else
				{
					StopFlying(false);
					transform.position = hit.point;
					visualiser.UpdateLinePosition(hit.point);

					return;
				}
			}

			var newPosition = ray.origin + ray.direction * distance;
			ray.origin = transform.position;
			transform.position = newPosition;
			visualiser.UpdateLinePosition(newPosition);

			// Out of range -- hit nothing
			if ((effectiveRange -= distance) <= 0f)
			{
				StopFlying(false);
				visualiser.HideBullet();
			}
		}
	}
}