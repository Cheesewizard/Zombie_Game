using Game.Scripts.Gameplay.Player;
using Game.Scripts.Gameplay.Services;
using Quack.ReferenceMagic.Runtime;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Animator
{
	public class PlayerMovementAnimator : MonoBehaviour, IDependencyInjectionCompleteHandler
	{
		[Inject]
		private PlayerAnimationAccessService animationAccessService;

		[SerializeField, Find(Destination.Ancestors)]
		private PlayerRig playerRig;

		/*
		 * Implement strategy pattern to do animations, such as pistol strategy, bat strategy etc
		 */

		public void HandleDependencyInjectionComplete()
		{
			playerRig.PlayerMovementBehaviour.OnMovement += HandleMovementAnimation;
			playerRig.PlayerMovementBehaviour.OnKilled += HandleDeathAnimation;
		}

		private void HandleMovementAnimation(bool isWalking)
		{
			animationAccessService.PlayerAnimator.SetBool("IsWalking", isWalking);
		}

		private void HandleDeathAnimation()
		{
			// This uses a state behaviour within the animator that deletes the gameObject after the death animation.
			animationAccessService.PlayerAnimator.SetTrigger("Death");
		}

		private void OnDestroy()
		{
			playerRig.PlayerMovementBehaviour.OnMovement -= HandleMovementAnimation;
			playerRig.PlayerMovementBehaviour.OnKilled -= HandleDeathAnimation;
		}
	}
}