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
            gun.OnShoot += Shoot;
            playerController.OnMovement += Movement;
            playerController.OnKilled += Death;
        }

        private void Shoot()
        {
            animator.SetTrigger("IsPistolAttack");
        }

        private void Melee()
        {
            animator.SetTrigger("IsKnifeAttack");
        }

        private void Movement(Vector2 movement)
        {
            animator.SetFloat("Speed", Mathf.Abs(movement.x + movement.y));
        }

        private void Death()
        {
            // This uses a state behaviour within the animator that deletes the gameObject after the death animation.
            animator.SetTrigger("Death");
        }

        private void OnDestroy()
        {
            gun.OnShoot += Shoot;
            playerController.OnMovement -= Movement;
            playerController.OnKilled -= Death;
        }
    }
}