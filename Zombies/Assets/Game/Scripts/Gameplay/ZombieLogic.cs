﻿using System;
using Assets.Scripts.Interfaces;
using Game.Scripts.Gameplay.Movement;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Zombieland.Gameplay.Enemies;

namespace Game.Scripts.Gameplay
{
    public class ZombieLogic : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Ancestors)]
        private SpriteRenderer spriteRenderer;

        [SerializeField, Required, Find(Destination.Self)]
        private ZombieHealth zombieHealth;
        public ZombieHealth ZombieHealth => zombieHealth;

        [SerializeField]
        private float knockback = 0.5f;

        public Action OnAttack;

        public void KillSelf()
        {
            gameObject.SetActive(false);
        }

        // May not keep in, I just like seeing the dead Zombies on the ground.
        public void OnDisable()
        {
            gameObject.GetComponent<ZombieMovementBehaviour>().enabled = false;
            gameObject.GetComponent<ZombieLogic>().enabled = false;

            //DeathKnockback();
            spriteRenderer.sortingOrder = 1;
            //gameObject.transform.Rotate(transform.position, Random.Range(0, 360), Space.Self);
        }

        private void DeathKnockback()
        {
            var rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(-transform.position * 200, ForceMode2D.Impulse);
            rb.drag = 2;

            rb.bodyType = RigidbodyType2D.Static;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnAttack?.Invoke();
                var player = collision.gameObject.GetComponent<IDamageable>();
                //player.GetDamage(attack);
            }
        }

        public float GetThreatLevel()
        {
            throw new System.NotImplementedException();
        }

        public void TakeKnockBack(GameObject other, float power)
        {
            Vector2 direction = (transform.position - other.transform.position).normalized;
            Vector2 force = direction * power * 10;

            //gameObject.GetComponent<Rigidbody2D>().AddForce(other.transform.position, ForceMode2D.Impulse);
            //gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        public float GetEnemyKnockback()
        {
            return this.knockback;
        }
    }
}