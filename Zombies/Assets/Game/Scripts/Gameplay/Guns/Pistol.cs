using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Weapons;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class Pistol : Gun, IUsableWeapon
	{
		public int WeaponId => gunConfig.WeaponId;

		private float FireRate => gunConfig.FireRate;
		private float nextFire = 0f;

		private void Start()
		{
			base.Init(gunConfig);
		}

		public void PerformAction()
		{
			Fire();
		}

		protected override void Fire()
		{
			if (Time.time - FireRate < nextFire) return;
			nextFire = Time.time - Time.deltaTime;

			RaiseUseWeaponEvent(gunConfig);
			bullet.ResetTransform(transform);
			bullet.Launch(this);
			RaiseFinishWeaponEvent();
		}

		protected override void Reload()
		{
			throw new NotImplementedException();
		}

		protected override void Activate()
		{
			RaiseOnActivatedEvent(this);
		}

		protected override void Deactivate()
		{
			throw new NotImplementedException();
		}
	}
}