using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;

namespace Game.Scripts.Gameplay.Guns
{
	public abstract class Gun : MonoBehaviour
	{
		[SerializeField]
		protected GunConfig gunConfig;
		public GunConfig GunConfig => gunConfig;

		[SerializeField, Required, Find(Destination.Self)]
		protected Bullet bullet;

		private GunHoldable gunHoldable;

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
			bullet.ResetTransform(gunConfig.SpawnPosition);
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