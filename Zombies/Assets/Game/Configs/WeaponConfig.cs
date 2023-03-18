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
        [SerializeField,  ValueDropdown(OdinDropdowns.WEAPONS)]
        private int weaponId;
        public int WeaponId => weaponId;

        [Title("Prefab", "The prefab containing the weapon.")]
        [SerializeField]
        private GameObject prefab;
        public GameObject Prefab => prefab;
        
        [SerializeField]
        private int upgradePrice;
        public int UpgradePrice => upgradePrice;
        
        [Title("Stats", "Basic stats for this weapon")]
        [SerializeField, HideLabel]
        private GunStats weaponStats;
        public GunStats Stats => weaponStats;

        [SerializeField]
        private RangedValue fireRate = new RangedValue(0f, 100f);
        public RangedValue FireRate => fireRate;

        [SerializeField]
        private float impactForce = 1f;
        public float ImpactForce => impactForce;

        public void SetWeaponLevel(int gunLevel)
        {
            WeaponLevel = gunLevel;
        }
    }
}