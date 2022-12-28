using Game.Scripts.Characters.Player;
using Game.Scripts.Gameplay.Guns;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Animator
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Self)]
        private UnityEngine.Animator animator;

        [SerializeField, Required, Find(Destination.AllChildren)]
        private Gun gun;

        [SerializeField, Required, Find(Destination.AllChildren)]
        private PlayerLogic playerLogic;

        private void Awake()
        {
            gun.OnShoot += HandleShootAnimation;
            playerLogic.OnMovement += HandleMovementAnimation;
            playerLogic.OnKilled += HandleDeathAnimation;
        }

        private void HandleShootAnimation(GunConfig gunConfig)
        {
            animator.SetTrigger("IsPistolAttack");
        }

        private void Melee()
        {
            animator.SetTrigger("IsKnifeAttack");
        }

        private void HandleMovementAnimation(bool isWalking)
        {
            animator.SetBool("IsWalking", isWalking);
        }

        private void HandleDeathAnimation()
        {
            // This uses a state behaviour within the animator that deletes the gameObject after the death animation.
            animator.SetTrigger("Death");
        }

        private void OnDestroy()
        {
            gun.OnShoot -= HandleShootAnimation;
            playerLogic.OnMovement -= HandleMovementAnimation;
            playerLogic.OnKilled -= HandleDeathAnimation;
        }
    }
}