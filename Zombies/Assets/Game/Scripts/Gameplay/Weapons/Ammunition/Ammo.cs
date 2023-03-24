using System;
using DUCK.Utils;
using Game.Scripts.Gameplay.Guns;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Ammunition
{
	public abstract class Ammo : ResetableObject
	{
		public abstract bool IsValid { get; }
		public abstract uint GetBulletId();
		public abstract uint GetDetonationId();

		public bool IsLoaded { get; protected set; }
		public Gun FiringGun { get; private set; }

		public event Action<Ammo, bool> OnStopFlying;

		public abstract SpriteRenderer CaseRenderer { get; }

		public abstract void HandleDetonation(Gun gun);

		public void GetLoaded(Transform parent)
		{
			if (parent != null)
			{
				transform.SetParent(parent, false);
				transform.Reset(true);
			}
			gameObject.SetActive(true);
			IsLoaded = true;
		}

		protected void HandleStopFlying(bool isHit)
		{
			OnStopFlying?.Invoke(this, isHit);
		}

		public void Detonate(Gun gun)
		{
			IsLoaded = false;
			FiringGun = gun;
			HandleDetonation(gun);
		}
	}
}