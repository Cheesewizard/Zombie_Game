using System;
using UnityEngine;

namespace Game.Configs
{
    [Serializable]
    public class WeaponStats
    {
        [SerializeField]
        private float damage;
        public float Damage => damage;

        [SerializeField]
        private float criticalDamagePercentage;
        public float CriticalDamagePercentage => criticalDamagePercentage;

        [SerializeField]
        private float criticalChancePercentage;
        public float CriticalChancePercentage => criticalChancePercentage;
    }
}