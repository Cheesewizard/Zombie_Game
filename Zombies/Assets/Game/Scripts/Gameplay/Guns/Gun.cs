using System;
using UnityEngine;
using Game.Configs;
using Game.Scripts.Gameplay.Weapons;
using Game.Scripts.Gameplay.Weapons.Ammunition;
using Game.Scripts.Gameplay.Weapons.Magazines;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;

namespace Game.Scripts.Gameplay.Guns
{
	public abstract class Gun : Weapon
	{
		[SerializeField]
		protected GunConfig gunConfig;
		public GunConfig GunConfig => gunConfig;

		[TitleGroup("References")]
		[SerializeField, Required]
		protected Transform bulletLaunchPoint;

		[SerializeField, Required, Find(Destination.AllChildren)]
		private Transform magazineParent;

		protected abstract Ammo LoadedAmmo { get; }

		public MagazineReloader Reloader;
		protected Magazine LoadedMagazine { get; set; }

		public override int WeaponId => gunConfig.WeaponId;
		
		// Properties

		public bool IsReadyToFire;
		public bool LockedReload { get; set; }
		public bool LockedFiring { get; set; }

		protected bool isReloading;
		public bool IsReloading => isReloading;
		
		public bool CanBeReloaded => isReloading && LoadedMagazine == null && !LockedReload;

		public event Action OnFired;
		public event Action<Ammo> OnAmmoFired;
		public event Action OnReload;
		public event Action OnEject;

		protected void InitGun(GunConfig gunConfig)
		{
			this.gunConfig = gunConfig;

			// Could have a player belt, for now though this will do
			Reloader.Init(GunConfig.MagazineConfig);
			LoadedMagazine = Reloader.TakeMagazine();
			LoadedMagazine.InstantLoad(magazineParent);
		}
		
		public void UpdateTrigger()
		{
			if (!LockedFiring && TryUseWeapon())
			{
				Fire();
			}
		}
		
		protected abstract bool TryUseWeapon();
		protected abstract void Fire();

		public abstract void TryToReload();
		protected abstract void Reload();

		protected void DetonateLoadedAmmo()
		{
			if (LoadedAmmo == null)
			{
				Debug.LogError("Cannot detonate a loaded ammo! Ammo is null, somehow.");
				return;
			}

			// SFX!
			LoadedAmmo.Detonate(this);
			OnFired?.Invoke();
			OnAmmoFired?.Invoke(LoadedAmmo);
		}

		protected bool LoadMagazine()
		{
			if (!CanBeReloaded) return false;
			
			LoadedMagazine = Reloader.TakeMagazine();
			LoadedMagazine.InstantLoad(magazineParent);
			OnReload?.Invoke(); 
			// SFX: insert mag
			return true;
		}
	}
}