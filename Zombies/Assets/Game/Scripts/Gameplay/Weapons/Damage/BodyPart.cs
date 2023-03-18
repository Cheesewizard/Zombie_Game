using System;
using Assets.Scripts.Interfaces;
using Game.Scripts.Gameplay.Guns;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Damage
{
    [Serializable]
    public class BodyPart: MonoBehaviour, BulletTarget.IBulletHitReceiver, IDamageable
    {
        [SerializeField]
        private BodyPartType bodyPartType;
        public BodyPartType BodyPartType => bodyPartType;

        public float DamageTakenMultiplier { get; }
        public float CriticalChancePercentage { get; }

        public Action<ZombieDamageInfo> OnBulletHit;

        void BulletTarget.IBulletHitReceiver.HandleBulletHit(Bullet bullet, RaycastHit2D hit)
        {
            var damageInfo = new ZombieDamageInfo(this, hit, bullet, true, bullet.Ray.direction);
            OnBulletHit?.Invoke(damageInfo);
        }
    }
}