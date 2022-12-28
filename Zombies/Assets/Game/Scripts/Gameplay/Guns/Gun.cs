using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.Gameplay.Guns
{
	public abstract class Gun : MonoBehaviour
	{
		[SerializeField]
		protected GunConfig gunConfig;

		private GunHoldable gunHoldable;

		private bool canShoot;

		public Action<GunConfig> OnShoot;
		public Action OnFinishShoot;

		protected virtual void Init(GunConfig gunConfig)
		{
			this.gunConfig = gunConfig;
		}

		protected virtual async void Shoot()
		{
			OnShoot?.Invoke(gunConfig);

			await UniTask.Delay(TimeSpan.FromSeconds(gunConfig.FireRate));

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