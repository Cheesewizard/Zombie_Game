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

        public event Action<ZombieDamageInfo> OnBulletHit;
        public static event Action<ZombieDamageInfo, Bullet, RaycastHit2D> OnBodyPartHit;
        
        void BulletTarget.IBulletHitReceiver.HandleBulletHit(Bullet bullet, RaycastHit2D hit)
        {
            var damageInfo = new ZombieDamageInfo(this, hit, bullet, true, bullet.Ray.direction);
            OnBulletHit?.Invoke(damageInfo);
            OnBodyPartHit?.Invoke(damageInfo, bullet, hit);
        }
    }
}