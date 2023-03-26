using System;
using Game.Configs;
using Game.Save;
using Game.Scripts.Core;
using Game.Scripts.Gameplay.Player.Input;
using Game.Scripts.Gameplay.Weapons;
using Quack.ReferenceMagic.Runtime;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public abstract class AbstractPlayerWeaponBehaviour : MonoBehaviour
	{
		[SerializeField, Find(Destination.Self)]
		private PlayerRig playerRig;

		[SerializeField, Find(Destination.Self)]
		private GameplayHand primaryHand;
		public GameplayHand PrimaryHand => primaryHand;

		public Weapon PrimaryWeapon { get; protected set; }

		public virtual void Init(ILoadout loadout)
		{
		}

		protected Weapon CreateWeapon(int weaponId)
		{
			var weaponInfo = CoreScene.ContentLibrary.WeaponLibrary.GetWeaponInfo(weaponId);
			if (weaponInfo.TryGetConfigForLevel(0, out var gunConfig))
			{
				return CreateWeapon(gunConfig);
			}

			throw new Exception($"Cannot get config for level (Create Weapon). It doesn't exist");
		}

		private Weapon CreateWeapon(WeaponConfig weaponConfig)
		{
			if (playerRig.WeaponPositions.TryGetPosition(weaponConfig.WeaponId, out var parent))
			{
				var newWeapon = weaponConfig.InstantiateGun(parent);
				newWeapon.Init();
				newWeapon.OnActivated += HandleGunActivated;
				newWeapon.OnDeactivated += HandleGunDeactivated;
				//newWeapon.OnAmmoFired += HandleAnyGunFired;
				return newWeapon;
			}

			return null;
		}

		private static void HandleGunActivated(Weapon gun)
		{
			// if (gun.CanBeReloaded && gun.Reloader.HasAmmoLeft)
			// {
			//     gun.Reloader.Show();
			// }
		}

		private static void HandleGunDeactivated(Weapon gun)
		{
			//gun.Reloader.Hide();
		}
	}
}