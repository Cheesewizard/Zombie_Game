using Game.Scripts.Gameplay;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Animator
{
    public class ZombieAnimator : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Self)]
        private UnityEngine.Animator animator;

        [SerializeField, Required, Find(Destination.AllChildren)]
        private ZombieLogic zombieLogic;

        private void Awake()
        {
            zombieLogic.OnAttack += HandleAttackAnimation;
            zombieLogic.OnKilled += HandleDeathAnimation;
        }

        private void HandleAttackAnimation()
        {
            animator.SetTrigger("Attack");
        }

        private void HandleDeathAnimation()
        {
            // This uses a state behaviour within the animator that deletes the gameObject after the death animation.
            animator.SetBool("Die", true);
        }

        private void OnDestroy()
        {
            zombieLogic.OnAttack -= HandleAttackAnimation;
            zombieLogic.OnKilled -= HandleDeathAnimation;
        }
    }
}