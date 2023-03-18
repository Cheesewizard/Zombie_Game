namespace Zombieland.Gameplay.Weapons.Damage
{
    public readonly struct DamageInfo
    {
        /// <summary>
        /// Amount of damage before modified by health pool damage transfer rate
        /// </summary>
        public float RawDamage { get; }

        /// <summary>
        /// Amount of damage that actually dealt to the core health
        /// </summary>
        public float DamageDealt { get; }

        /// <summary>
        /// Was the damage a critical hit?
        /// </summary>
        public bool IsCriticalHit { get; }

        /// <summary>
        /// Was the damage a minor hit?
        /// </summary>
        public bool IsMinorHit { get; }

        public DamageInfo(float rawDamage, float damageDealt, bool isCriticalHit)
        {
            RawDamage = rawDamage;
            DamageDealt = damageDealt;
            IsCriticalHit = isCriticalHit;
            IsMinorHit = damageDealt < rawDamage;
        }
    }
}