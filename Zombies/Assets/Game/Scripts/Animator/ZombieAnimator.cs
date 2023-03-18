using Game.Scripts.Gameplay;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Zombieland.Gameplay.Enemies;

namespace Game.Scripts.Animator
{
    public class ZombieAnimator : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Self)]
        private UnityEngine.Animator animator;

        [SerializeField, Required, Find(Destination.AllChildren)]
        private ZombieLogic zombieLogic;

        [SerializeField, Required, Find(Destination.AllChildren)]
        private ZombieHealth zombieHealth;

        private void Awake()
        {
            zombieLogic.OnAttack += HandleAttackAnimation;
            zombieHealth.OnKilled += HandleDeathAnimation;
        }

        private void HandleAttackAnimation()
        {
            animator.SetTrigger("Attack");
        }

        private void HandleDeathAnimation(ZombieDamageInfo _)
        {
            // This uses a state behaviour within the animator that deletes the gameObject after the death animation.
            animator.SetBool("Die", true);
        }

        private void OnDestroy()
        {
            zombieLogic.OnAttack -= HandleAttackAnimation;
            zombieHealth.OnKilled -= HandleDeathAnimation;
        }
    }
}