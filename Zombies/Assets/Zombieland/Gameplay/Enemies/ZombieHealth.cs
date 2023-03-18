using System;
using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.Weapons.Damage;
using UnityEngine;

namespace Zombieland.Gameplay.Enemies
{
    public class ZombieHealth : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth = 100f;

        public float MaxHealth => maxHealth;

        public float CurrentHealth { get; private set; }

        [SerializeField]
        private BodyPart[] bodyPart;

        public Action<ZombieDamageInfo> OnDamaged;
        public Action<ZombieDamageInfo> OnHealthPoolDepleted;
        public Action<ZombieDamageInfo> OnKilled;

        private void Awake()
        {
            CurrentHealth = maxHealth;
            foreach (var bodypart in bodyPart)
            {
                bodypart.OnBulletHit += HandleBulletHit;
            }
        }

        private void HandleBulletHit(ZombieDamageInfo damageInfo)
        {
            TakeDamage(damageInfo);
        }

        public void TakeDamage(ZombieDamageInfo damageInfo)
        {
            var damage = damageInfo.DamageSource.Damage;
            CurrentHealth -= damage;

            OnDamaged?.Invoke(damageInfo);

            if (CurrentHealth <= 0f)
            {
                OnHealthPoolDepleted?.Invoke(damageInfo);
                OnKilled?.Invoke(damageInfo);
            }
        }
    }
}