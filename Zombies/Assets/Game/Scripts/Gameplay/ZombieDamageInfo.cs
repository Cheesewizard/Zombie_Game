using Assets.Scripts.Interfaces;
using Game.Scripts.Gameplay.Weapons.Damage;
using UnityEngine;
using Zombieland.Gameplay.Weapons.Damage;

namespace Game.Scripts.Gameplay
{
    public class ZombieDamageInfo
    {
        // Immutable Info
        public BodyPart BodyPart { get; }
        public RaycastHit2D Hit { get; }
        public IDamageDealer DamageSource { get; }
        public Vector3 DamageDirection { get; }
        public bool IsBulletDamage { get; }

        // Info that can be added / modified later on
        public DamageInfo DamageInfo { get; set; }
        public bool WasBodyPartAliveOnImpact { get; set; }
        public bool IsKillingBlow { get; set; }
        public bool IsDodged { get; set; }
        public bool HasTriggeredCss { get; set; }

        public ZombieDamageInfo(BodyPart bodyPart, RaycastHit2D hit, IDamageDealer damageSource, bool isBulletDamage, Vector3 damageDirection = default)
        {
            BodyPart = bodyPart;
            Hit = hit;
            DamageSource = damageSource;
            IsBulletDamage = isBulletDamage;
            DamageDirection = damageDirection;
        }
    }
}
