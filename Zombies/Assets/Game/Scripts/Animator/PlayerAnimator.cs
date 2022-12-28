using Game.Scripts.Characters.Player;
using Game.Scripts.Gameplay.Guns;
using UnityEngine;

namespace Game.Scripts.Animator
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.Animator animator;

        [SerializeField] 
        private Gun gun;
        
        [SerializeField] 
        private PlayerController playerController;

        private void Awake()
        {
         //   gun.OnShoot += HandleShootAnimation;
            playerController.OnMovement += HandleMovementAnimation;
            playerController.OnKilled += HandleDeathAnimation;
        }

        private void HandleShootAnimation(GunConfig gunConfig)
        {
            animator.SetTrigger("IsPistolAttack");
        }

        private void Melee()
        {
            animator.SetTrigger("IsKnifeAttack");
        }

        private void HandleMovementAnimation(Vector2 movement)
        {
            animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y)));
        }

        private void HandleDeathAnimation()
        {
            // This uses a state behaviour within the animator that deletes the gameObject after the death animation.
            animator.SetTrigger("Death");
        }

        private void OnDestroy()
        {
          //  gun.OnShoot -= HandleShootAnimation;
            playerController.OnMovement -= HandleMovementAnimation;
            playerController.OnKilled -= HandleDeathAnimation;
        }
    }
}