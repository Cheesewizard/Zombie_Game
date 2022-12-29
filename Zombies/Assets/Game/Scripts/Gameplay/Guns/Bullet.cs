using System;
using Game.Configs;
using Game.Scripts.Utils;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class Bullet : ResetableObject
	{
		[SerializeField, Required, Find(Destination.Self)]
		private BulletVisualiser visualiser;

		private Ray ray;
		private float bulletSpeed;
		private float effectiveRange;
		private bool isFlying;
		private Gun firingGun;

		public override void Init()
		{
			base.Init();
			visualiser.InitVisual();
			//penetrationPower = maxPenetrationPower;
		}

		public void ResetTransform(Transform parent)
		{
			base.ResetTransform(parent);

			isFlying = false;
			visualiser.InitVisual();

			//penetrationPower = maxPenetrationPower;
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
			//onStopFlying?.Invoke(hit);
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
			// impactForce = gun.GunConfig.ImpactForce;
			effectiveRange = gun.GunConfig.EffectiveRange;
			bulletSpeed = gun.GunConfig.BulletSpeed;
			// bloodHitEffect = gun.GunConfig.BloodHitFeedbackPrefab;

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

				//TODO: Material provider, sounds etc
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