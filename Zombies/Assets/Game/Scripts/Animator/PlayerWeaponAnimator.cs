using Game.Configs;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.Gameplay.Services;
using Quack.ReferenceMagic.Runtime;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Animator
{
	public class PlayerWeaponAnimator : MonoBehaviour, IDependencyInjectionCompleteHandler
	{
		[Inject]
		private PlayerAnimationAccessService animationAccessService;

		[SerializeField, Find(Destination.Ancestors)]
		private AbstractPlayerWeaponBehaviour playerGunBehaviour;
		
		public void HandleDependencyInjectionComplete()
		{
			playerGunBehaviour.PrimaryHand.OnGrabbed += HandleWeaponEquipped;
		}

		private void HandleWeaponEquipped(IHoldableItem item)
		{
			if (item.IsCurrentlyHeld)
			{
				playerGunBehaviour.PrimaryWeapon.OnUseWeapon += HandleShootAnimation;
			}
		}

		private void HandleShootAnimation(WeaponConfig weaponConfig)
		{
			animationAccessService.PlayerAnimator.SetTrigger("IsPistolAttack");
		}

		private void Melee()
		{
			animationAccessService.PlayerAnimator.SetTrigger("IsKnifeAttack");
		}

		private void OnDestroy()
		{
			playerGunBehaviour.PrimaryWeapon.OnUseWeapon -= HandleShootAnimation;
		}
	}
}