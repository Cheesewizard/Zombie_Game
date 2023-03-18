using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Scripts.Gameplay.Weapons;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;

namespace Game.Scripts.Gameplay.Guns
{
	public abstract class Gun : MonoBehaviour
	{
		[TitleGroup("References")]
		[SerializeField, Required, Find(Destination.Ancestors)]
		private WeaponHoldable weaponHoldable;
		public WeaponHoldable WeaponHoldable => weaponHoldable;
		
		[SerializeField]
		protected GunConfig gunConfig;
		public GunConfig GunConfig => gunConfig;

		[SerializeField, Required, Find(Destination.Self)]
		protected Bullet bullet;

		[SerializeField] 
		private Transform spawnPosition;
		public Transform SpawnPosition => spawnPosition;

		private bool canShoot = true;

		public Action<GunConfig> OnShoot;
		public Action OnFinishShoot;

		protected virtual void Init(GunConfig gunConfig)
		{
			this.gunConfig = gunConfig;
			//TODO Change this to be part of the pooling system
			bullet.Init();
		}

		protected virtual async UniTask Shoot()
		{
			if(!canShoot) return;

			OnShoot?.Invoke(gunConfig);
			bullet.ResetTransform(SpawnPosition);
			bullet.Launch(this);

			// TODO: I dont like this arbituary delay, need a better design
			await UniTask.Delay(TimeSpan.FromSeconds(gunConfig.FireRate.Random()));
			canShoot = true;
			OnFinishShoot?.Invoke();
		}

		protected virtual void Reload()
		{
		}

		protected void IsEnabled()
		{
		}
	}
}