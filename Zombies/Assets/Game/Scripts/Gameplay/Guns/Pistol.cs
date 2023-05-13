using System;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.Gameplay.Weapons.Ammunition;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class Pistol : Gun
	{
		public int WeaponId => gunConfig.WeaponId;

		protected override Ammo LoadedAmmo => loadedCartridge;
		private Cartridge loadedCartridge;

		private float FireRate => gunConfig.FireRate;
		private float nextFire = 0f;

		public override void Init(PlayerBelt belt)
		{
			InitGun(belt, gunConfig);
			TryLoadAmmo();
			IsReadyToFire = true;
		}

		protected override bool TryUseWeapon()
		{
			if (IsReadyToFire)
			{
				Fire();
				return true;
			}

			return false;
		}

		protected override void Fire()
		{
			if (Time.time - FireRate < nextFire) return;
			nextFire = Time.time - Time.deltaTime;
			IsReadyToFire = false;

			RaiseUseWeaponEvent(gunConfig);
			DetonateLoadedAmmo();
			RaiseFinishWeaponEvent();
			HandleOnFireComplete();
		}

		public override void TryToReload()
		{
			Reload();
		}

		protected override void Reload()
		{
			isReloading = true;
			if (LoadMagazine())
			{
				IsReadyToFire = true;
			}

			isReloading = false;
		}

		protected bool TryLoadAmmo()
		{
			if (isReloading) return false;

			loadedCartridge = LoadedMagazine.RequestAmmo<Cartridge>();
			if (loadedCartridge != null)
			{
				loadedCartridge.GetLoaded(bulletLaunchPoint);
				IsReadyToFire = true;
				isReloading = false;
				return true;
			}

			// SFX
			return false;
		}

		protected override void Activate()
		{
			RaiseOnActivatedEvent(this);
		}

		protected override void Deactivate()
		{
			throw new NotImplementedException();
		}

		// Probably needs moving into the gun class
		protected void HandleOnFireComplete()
		{
			if (TryLoadAmmo())
			{
				IsReadyToFire = true;
			}
		}
	}
}