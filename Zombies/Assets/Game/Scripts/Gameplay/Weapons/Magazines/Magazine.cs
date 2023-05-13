using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.Weapons.Ammunition;
using Game.Scripts.Utils;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class Magazine : MonoBehaviour
	{
		// shell physics
		// Shell despawn timer

		[SerializeField]
		private bool isAutoPooled = true;

		[SerializeField, ShowIf(nameof(isAutoPooled))]
		private int autoPoolCapacity = 7;

		[SerializeField, ShowIf(nameof(isAutoPooled)), Find(Destination.AllChildren)]
		private Ammo ammoPrefab;

		[SerializeField, Find(Destination.AllChildren)]
		private Ammo[] serializedAmmo;
		
		[SerializeField]
		private new Rigidbody2D rigidbody;

		public int AmmoLeft { get; private set; }
		public int Capacity => serializedAmmo.Length;

		private readonly List<Ammo> ammoPool = new();
		private Ammo previouslyLoadedAmmo;

		private bool isInGun;

		private new Transform transform;
		private Transform originalParent;

		public void Init()
		{
			transform = base.transform;
			originalParent = transform.parent;

			if (isAutoPooled)
			{
				ammoPrefab.gameObject.SetActive(false);
				// We create 1 extra ammo than we needed just for the extra one in the chamber
				for (var i = 0; i <= autoPoolCapacity; ++i)
				{
					var ammo = Instantiate(ammoPrefab, transform);
					ammo.name = ammo.name + "-" + i;
					ammo.Init();
					ammoPool.Add(ammo);
				}

				ammoPool[0].gameObject.SetActive(true);
			}
			else
			{
				ammoPool.AddRange(serializedAmmo);
				foreach (var ammo in ammoPool)
				{
					ammo.Init();
					ammo.gameObject.SetActive(true);
				}
			}
			
			gameObject.SetActive(false);
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
		
		public void AttachToReloader(Transform target)
		{
			ReParent(target);
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
		
		public void InstantEjectMagazine()
		{
			isInGun = false;
			if (rigidbody != null)
			{
				rigidbody.Detach();
			}
			else
			{
				transform.DetatchFromParent();
			}
		}

		public void InstantLoad(Transform parent)
		{
			isInGun = true;
			ReParent(parent);
		}

		private void ReParent(Transform target)
		{
			gameObject.SetActive(true);
			
			if (rigidbody != null)
			{
				rigidbody.Attach(target);
			}
			else
			{
				transform.SetParent(target, true);
			}
		}
		
		public void ResetTransforms()
		{
			previouslyLoadedAmmo = null;
			// Reset all (not loaded) ammo
			foreach (var ammo in ammoPool)
			{
				// Remember which one was the loaded one
				if (ammo.IsLoaded)
				{
					previouslyLoadedAmmo = ammo;
				}
				else
				{
					ammo.ResetTransform(transform);
				}
			}

			// Back to the original parent if it is not in a gun
			if (!isInGun)
			{
				transform.SetParent(originalParent);
			}
		}
	}
}