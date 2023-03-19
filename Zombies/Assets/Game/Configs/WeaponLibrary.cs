using System;
using System.Collections.Generic;
using Game.Configs;
using Game.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Config
{
    [Serializable]
    public class WeaponLibrary
    {
        [SerializeField]
        private WeaponInfo[] weapons = new WeaponInfo[0];
        private readonly Dictionary<int, WeaponInfo> weaponsDictionary = new Dictionary<int, WeaponInfo>();

        public WeaponInfo[] Weapons => weapons;

        public void Init()
        {
            foreach (var weapon in weapons)
            {
                weaponsDictionary.Add(weapon.WeaponId, weapon);
            }
        }

        public WeaponInfo GetWeaponInfo(int weaponId)
        {
            if (!weaponsDictionary.TryGetValue(weaponId, out var gunInfo))
            {
                throw new Exception($"Invalid gun Id {weaponId}");
            }

            return gunInfo;
        }

        public bool TryGetWeaponInfo(int weaponId, out WeaponInfo weaponInfo) => weaponsDictionary.TryGetValue(weaponId, out weaponInfo);
    }
    
    
    [Serializable]
    public class WeaponInfo
    {
        [Title("Core Settings", "These settings define the weapon.")]
        [SerializeField, ValueDropdown(OdinDropdowns.WEAPONS)]
        private int weaponId;
        public int WeaponId => weaponId;

        [SerializeField]
        private string displayName;
        public string DisplayName => displayName;

        [SerializeField]
        private GunConfig[] weaponLevels;

        public int WeaponMaxLevel => weaponLevels.Length;

        public void Init()
        {
            for (var i = 0; i < weaponLevels.Length; i++)
            {
                weaponLevels[i].SetWeaponLevel(i);
            }
        }

        public bool TryGetConfigForLevel(int weaponLevel, out WeaponConfig weaponConfig)
        {
            if (weaponLevel >= 0 && weaponLevels.Length > weaponLevel)
            {
                weaponConfig = weaponLevels[weaponLevel];
                return true;
            }

            weaponConfig = null;
            return false;
        }
    }
}