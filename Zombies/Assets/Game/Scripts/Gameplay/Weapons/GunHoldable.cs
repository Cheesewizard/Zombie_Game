using System;
using Game.Scripts.Gameplay.Guns;
using Game.Scripts.Gameplay.Services;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
	public class GunHoldable : WeaponHoldable
	{
		[SerializeField, Required, Find(Destination.Self)]
		private Gun gun;

		private void Awake()
		{
			gun.OnFired += HandleGunFired;
			gun.OnEject += HandleMagazineEjected;
			gun.OnReload += HandleMagazineReloaded;
		}

		private void HandleMagazineReloaded()
		{
			throw new NotImplementedException();
		}

		private void HandleMagazineEjected()
		{
			throw new NotImplementedException();
		}

		private void HandleGunFired()
		{
			//throw new NotImplementedException();
		}

		public override void UpdateInput(PlayerInputConsumerAccessService playerInput)
		{
			if (playerInput.InputConsumer.Player.Shoot.IsPressed())
			{
				Debug.Log("Firing Input");
				gun.UpdateTrigger();
			}

			if (playerInput.InputConsumer.Player.Reload.WasPressedThisFrame())
			{
				Debug.Log("Reloading Input");
				gun.TryToReload();
			}
		}

		private void OnDestroy()
		{
			gun.OnFired -= HandleGunFired;
			gun.OnEject -= HandleMagazineEjected;
			gun.OnReload -= HandleMagazineReloaded;
		}
	}
}