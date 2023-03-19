using System.Collections.Generic;
using Game.Config;
using Game.Scripts.Core;
using Game.Scripts.Core.Loading;
using Game.Scripts.Gameplay.Guns;
using Game.Scripts.Gameplay.Weapons.Damage;
using Game.Scripts.Gameplay.WorldObjects;
using Quack.Utils;
using Quack.Utils.Pooling;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Gameplay.Services
{
	public class BulletImpactService : ISceneInitializedHandler, IDependencyDestructionHandler
	{
		[Inject]
		private IPlayerWeaponBehaviourAccessService gameplayPlayerAccessService = null;

		private PrefabPool bloodVfxImpactPool;

		private const int MAXIMUM_ACTIVE_PARTICLE_LIMIT = 36;
		private const int MAXIMUM_ACTIVE_DECAL_LIMIT = 48;

		private readonly BulletImpactDataLibrary weaponImpactLibrary;

		/*
		 A pool of particles that are linked to different gun ids and materials. It will instantiate all particle systems
		 on scene initialised, this way we will have a number of different particle systems that we will be able to reuse.
		*/

		private readonly List<BulletParticlePool> particlePools = new();
		private readonly List<BulletDecalPool> decalPools = new();

		private readonly Queue<ParticleSystem> activeParticlesQueue = new();
		private readonly Queue<Sprite> activeDecalsQueue = new();


		public BulletImpactService()
		{
			SceneBroadcaster.RegisterReceiver(this);
			weaponImpactLibrary = CoreScene.ContentLibrary.BulletImpactDataLibrary;
		}

		public void HandleSceneInitialized()
		{
			//Due to the particle systems getting destroyed on ending a scene we want to create them every time the scene is initialised.
			CreateParticleAndDecalPools();
		}

		private void CreateParticleAndDecalPools()
		{
			var weaponBehaviour = gameplayPlayerAccessService.WeaponBehaviour;

			if (weaponBehaviour.PrimaryWeapon != null)
			{
				var primaryWeaponId = weaponBehaviour.PrimaryWeapon.WeaponId;

				//If we can't find it default to the max.
				if (!weaponImpactLibrary.TryGetMaxParticleCount(primaryWeaponId, out var maxParticleCount))
				{
					maxParticleCount = MAXIMUM_ACTIVE_PARTICLE_LIMIT;
				}

				if (!weaponImpactLibrary.TryGetMaxDecalCount(primaryWeaponId, out var maxDecalCount))
				{
					maxDecalCount = MAXIMUM_ACTIVE_DECAL_LIMIT;
				}

				//Create the particle systems for the primary gun.
				if (weaponImpactLibrary.TryGetWeaponImpactData(primaryWeaponId, out var primaryWeaponParticleData))
				{
					foreach (var impactData in primaryWeaponParticleData)
					{
						CreateParticleSystems(impactData.ParticleSystem, impactData.Material, maxParticleCount,
							primaryWeaponId);
						//CreateDecals(impactData.DecalProjector, impactData.Material, maxDecalCount, primaryWeaponId);
					}
				}
			}
		}

		//Create the particle systems for the object pools.
		private void CreateParticleSystems(ParticleSystem particleSystem, ColliderSurfaceMaterial material,
			int maxParticleCount, int gunId)
		{
			if (particleSystem == null) return;

			var particleQueue = new Queue<ParticleSystem>();

			//Create the wanted amount of particles for each pool.
			for (var index = 0; index < maxParticleCount; index++)
			{
				var particle = Object.Instantiate(particleSystem);
				particle.Stop();
				particleQueue.Enqueue(particle);
			}
		}

		//Create the particle systems for the object pools.
		// private void CreateDecals(Sprite decalProjector, ColliderSurfaceMaterial material,
		// 	int maxDecalCount, int gunId)
		// {
		// 	if (decalProjector == null) return;
		//
		// 	var decalQueue = new Queue<Sprite>();
		//
		// 	//Create the wanted amount of particles for each pool.
		// 	for (var index = 0; index < maxDecalCount; index++)
		// 	{
		// 		var decal = Object.Instantiate(decalProjector);
		//
		// 		decal.enabled = false;
		// 		decalQueue.Enqueue(decal);
		// 	}
		//
		// 	//Create the pool to the list of particle pools.
		// 	decalPools.Add(new BulletDecalPool(gunId, material, decalQueue));
		// }

		public void PlayParticle(Transform targetTransform, Vector3 targetPosition, Quaternion targetRotation, int weaponId,
			ColliderSurfaceMaterial material, bool shouldParent)
		{
			// Find the already instantiated particle and use default if not found.
			var pool = particlePools.Find(particlePool =>
				           particlePool.WeaponId == weaponId && particlePool.ColliderSurfaceMaterial == material) ??
			           particlePools.Find(particlePool =>
				           particlePool.WeaponId == weaponId &&
				           particlePool.ColliderSurfaceMaterial == ColliderSurfaceMaterial.Default);

			if (pool == null)
			{
				Debug.LogError(
					"Tried to create a particle but no reference in the content library to it, no default set either.");
				return;
			}

			// If there are currently too many particles active, stop the oldest one.
			if (activeParticlesQueue.Count >= MAXIMUM_ACTIVE_PARTICLE_LIMIT)
			{
				// Dequeue and stop the oldest particle if it's playing.
				var oldestParticle = activeParticlesQueue.Dequeue();
				if (oldestParticle.IsAlive(true))
				{
					oldestParticle.Clear(true);
				}
			}

			// Pop the oldest particle out of the pool to reuse it.
			var particleSystem = pool.ParticleSystems.Dequeue();

			// Make sure to stop before playing in the case that it's already playing for an old impact.
			if (particleSystem.IsAlive(true))
			{
				particleSystem.Clear(true);
			}

			particleSystem.transform.SetParent(shouldParent ? targetTransform : null);

			// Set the position and rotation of the particle system.
			particleSystem.transform.position = targetPosition;
			particleSystem.transform.rotation = targetRotation;

			// Play the particle
			particleSystem.Play();

			// Place it back in the active particles queue, so it can be removed when it is the oldest particle.
			activeParticlesQueue.Enqueue(particleSystem);

			// Put it back in the pool to be reused.
			pool.ParticleSystems.Enqueue(particleSystem);
		}


		public void PlaySound(int weaponId, ColliderSurfaceMaterial material)
		{
			if (weaponImpactLibrary.TryGetSoundFX(weaponId, material, out var audioSource))
			{
				audioSource.Play();
			}
		}

		public void RefreshPool()
		{
			particlePools.Clear();
			decalPools.Clear();

			activeParticlesQueue.Clear();
			activeDecalsQueue.Clear();

			CreateParticleAndDecalPools();
		}

		public void HandleDependenciesDestroyed()
		{
			bloodVfxImpactPool?.Destroy();
		}
	}

	public class BulletParticlePool
	{
		public BulletParticlePool(int weaponId, ColliderSurfaceMaterial colliderSurfaceMaterial,
			Queue<ParticleSystem> particleSystems)
		{
			WeaponId = weaponId;
			ColliderSurfaceMaterial = colliderSurfaceMaterial;
			ParticleSystems = particleSystems;
		}

		public int WeaponId { get; }
		public ColliderSurfaceMaterial ColliderSurfaceMaterial { get; }
		public Queue<ParticleSystem> ParticleSystems { get; }
	}

	public class BulletDecalPool
	{
		public BulletDecalPool(int waponId, ColliderSurfaceMaterial colliderSurfaceMaterial, Queue<Sprite> decals)
		{
			WeaponId = waponId;
			ColliderSurfaceMaterial = colliderSurfaceMaterial;
			Decals = decals;
		}

		public int WeaponId { get; }
		public ColliderSurfaceMaterial ColliderSurfaceMaterial { get; }
		public Queue<Sprite> Decals { get; }
	}
}