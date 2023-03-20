using System;
using Game.Scripts.Gameplay.Weapons;
using Game.Utils;
using Quack.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Configs
{
	public class WeaponConfig : ScriptableObject
	{
		public int WeaponLevel { get; private set; }

		[Title("Core Settings", "These settings define the weapon.")]
		[SerializeField, ValueDropdown(OdinDropdowns.WEAPONS)]
		private int weaponId;

		public int WeaponId => weaponId;

		[SerializeField]
		private string weaponPrefabPath;

		[Title("Prefab", "The prefab containing the weapon.")]
		[SerializeField]
		private GameObject prefab;

		public GameObject Prefab => prefab;


		[SerializeField]
		private int upgradePrice;

		public int UpgradePrice => upgradePrice;

		[Title("Stats", "Basic stats for this weapon")]
		[SerializeField, HideLabel]
		private WeaponStats weaponStats;

		public WeaponStats Stats => weaponStats;

		[SerializeField]
		private float fireRate;

		public float FireRate => fireRate;

		[SerializeField]
		private float impactForce = 1f;

		public float ImpactForce => impactForce;

		public void SetWeaponLevel(int weaponLevel)
		{
			WeaponLevel = weaponLevel;
		}

		public Weapon InstantiateGun(Transform parent)
		{
			var weapon = Resources.Load(weaponPrefabPath) as GameObject;
			if (weapon == null)
				throw new NullReferenceException($"Failed to instantiate gun from resources, path: {weaponPrefabPath}");

			Instantiate(weapon, parent, false);
			return weapon.GetComponent<Weapon>();
		}
	}
}