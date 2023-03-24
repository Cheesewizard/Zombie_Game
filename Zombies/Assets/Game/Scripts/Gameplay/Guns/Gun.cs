using System;
using UnityEngine;
using Game.Configs;
using Game.Scripts.Gameplay.Weapons;
using Game.Scripts.Gameplay.Weapons.Ammunition;
using Game.Scripts.Gameplay.Weapons.Magazines;

namespace Game.Scripts.Gameplay.Guns
{
	public abstract class Gun : Weapon
	{
		[SerializeField]
		protected GunConfig gunConfig;

		public GunConfig GunConfig => gunConfig;

		protected abstract Ammo LoadedAmmo { get; }

		public MagazineReloader Reloader;
		protected Magazine LoadedMagazine { get; set; }

		public override int WeaponId => gunConfig.WeaponId;

		public event Action<Ammo> OnAmmoFired;
		public event Action OnReload;
		public event Action OnEject;

		protected void InitGun(GunConfig gunConfig)
		{
			this.gunConfig = gunConfig;

			// Could have a player belt, for now though this will do
			Reloader.Init(GunConfig.MagazineConfig);
			LoadedMagazine = Reloader.TakeMagazine();
			//LoadedMagazine.InstantLoad(magazineParent);
		}

		protected abstract void Fire();

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
			OnAmmoFired?.Invoke(LoadedAmmo);
		}
	}
}