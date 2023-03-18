using System;
using Game.Scripts.Gameplay.Guns;
using Quack.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Configs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Config/GunConfig")]
    public class GunConfig : WeaponConfig
    {
        [SerializeField] private GunTypes gunType;
        public GunTypes GunType => gunType;

        [SerializeField] private float effectiveRange = 100f;
        public float EffectiveRange => effectiveRange;

        [Title("Magazine", "The magazine that been used for the gun and its related configs.")]
        [SerializeField, HideLabel]
        private MagazineConfig magazineConfig;
        public MagazineConfig MagazineConfig => magazineConfig;

        [SerializeField, HideLabel] 
        private GunRecoilConfig recoilConfig;
        public GunRecoilConfig RecoilConfig => recoilConfig;

        [SerializeField] 
        private RangedValue bulletSpeed = new RangedValue(5f, 1000f);
        public RangedValue BulletSpeed => bulletSpeed;
    }
}