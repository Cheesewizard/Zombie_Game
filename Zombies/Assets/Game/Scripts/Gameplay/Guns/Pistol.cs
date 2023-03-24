using System;
using Game.Scripts.Gameplay.Weapons;
using Game.Scripts.Gameplay.Weapons.Ammunition;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class Pistol : Gun, IUsableWeapon
	{
		public int WeaponId => gunConfig.WeaponId;

		protected override Ammo LoadedAmmo => loadedCartridge;
		private Cartridge loadedCartridge;

		private float FireRate => gunConfig.FireRate;
		private float nextFire = 0f;

		public override void Init()
		{
			InitGun(gunConfig);
			TryLoadAmmo();
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
			DetonateLoadedAmmo();
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

		protected bool TryLoadAmmo()
		{
			//if (isReloading) return false;

			var loadedCartridge = LoadedMagazine.RequestAmmo<Cartridge>();
			if (loadedCartridge != null)
			{
				loadedCartridge.GetLoaded(transform);
				return true;
			}

			// SFX
			return false;
		}
	}
}