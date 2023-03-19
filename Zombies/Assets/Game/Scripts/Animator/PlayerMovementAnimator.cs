using Game.Scripts.Gameplay.Services;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Animator
{
	public class PlayerMovementAnimator : MonoBehaviour, IDependencyInjectionCompleteHandler
	{
		[Inject]
		private PlayerAnimationAccessService animationAccessService;

		[Inject]
		private GameplayPlayerAccessService playerAccessService;

		/*
		 * Implement strategy pattern to do animations, such as pistol strategy, bat strategy etc
		 */

		public void HandleDependencyInjectionComplete()
		{
			playerAccessService.PlayerRig.PlayerLogic.OnMovement += HandleMovementAnimation;
			playerAccessService.PlayerRig.PlayerLogic.OnKilled += HandleDeathAnimation;
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
			playerAccessService.PlayerRig.PlayerLogic.OnMovement -= HandleMovementAnimation;
			playerAccessService.PlayerRig.PlayerLogic.OnKilled -= HandleDeathAnimation;
		}
	}
}