using System.Collections.Generic;
using Game.Scripts.Gameplay.Weapons.Ammunition;
using Quack.ReferenceMagic.Runtime;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class Magazine : MonoBehaviour
	{
		// shell physics
		// Shell despawn timer

		[SerializeField,  Find(Destination.AllChildren)]
		private Ammo[] serializedAmmo;

		private readonly List<Ammo> ammoPool = new ();

		public int AmmoLeft { get; private set; }
		public int Capacity => serializedAmmo.Length;

		public void Init()
		{
			ammoPool.AddRange(serializedAmmo);
			foreach (var ammo in ammoPool)
			{
				ammo.Init();
				ammo.gameObject.SetActive(true);
			}
		}

		public void ModifyAmmoLeft(int amount = -1)
		{
			AmmoLeft = amount < 0 ? Capacity : Mathf.Min(amount, Capacity);

			for (var i = 0; i < AmmoLeft; i++)
			{
				var ammo = ammoPool[i];
				if (!ammo.IsLoaded)
				{
					ammo.ResetTransform(transform);
				}
			}
		}

		public T RequestAmmo<T>() where T : Ammo
		{
			if (AmmoLeft > 0)
			{
				--AmmoLeft;
				var ammo = ammoPool[AmmoLeft];
				return ammo as T;
			}

			// Not enough
			AmmoLeft = 0;
			return null;
		}
	}
}