using System;
using Quack.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Configs
{
    [Serializable]
    public class GunRecoilConfig
    {
        [SerializeField]
        private AnimationCurve sensitivityRange;
        public AnimationCurve SensitivityRange => sensitivityRange;

        [SerializeField]
        private AnimationCurve recoverSpeedRange;
        public AnimationCurve RecoverSpeedRange => recoverSpeedRange;

        [SerializeField, Range(-90f, 0f)]
        private float tiltAngle = -12f;
        public float TiltAngle => tiltAngle;

        [SerializeField]
        private RangedValue spreadAngleRange = new RangedValue(-3f, 3f);
        public RangedValue SpreadAngleRange => spreadAngleRange;

        [SerializeField]
        private RangedValue recoilDistanceRange = new RangedValue(0.01f, 0.05f);
        public RangedValue RecoilDistanceRange => recoilDistanceRange;

        [SerializeField]
        private float recoilCatchDuration = 0.06f;
        public float RecoilCatchDuration => recoilCatchDuration;

        [SerializeField, Range(0, 1f)]
        private float stabilizerRecoilMultiplier = 0.5f;
        public float StabilizerRecoilMultiplier => stabilizerRecoilMultiplier;

        [SerializeField]
        private float stabilizerRecoilTimeScale = 1.5f;
        public float StabilizerRecoilTimeScale => stabilizerRecoilTimeScale;

        [SerializeField]
        private bool canBeAffectedBySteadyPerk;
        public bool CanBeAffectedBySteadyPerk => canBeAffectedBySteadyPerk;

        [SerializeField, ShowIf(nameof(canBeAffectedBySteadyPerk)), Range(0, 1f)]
        private float steadyPerkRecoilMultiplier = 0.65f;
        public float SteadyPerkRecoilMultiplier => steadyPerkRecoilMultiplier;

        [SerializeField, ShowIf(nameof(canBeAffectedBySteadyPerk))]
        private float steadyPerkRecoilTimeScale = 1.35f;
        public float SteadyPerkRecoilTimeScale => steadyPerkRecoilTimeScale;
    }
}