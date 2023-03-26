using Game.Scripts.Gameplay.Player;
using Game.Scripts.Gameplay.Player.Input;
using Game.Scripts.Gameplay.Services;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
	public abstract class ItemHoldableItemAbstract : MonoBehaviour , IHoldableItem
	{
		public bool IsCurrentlyHeld { get; }
		
		[SerializeField]
		private bool canBeGrabbed = true;
		public bool CanBeGrabbed { get => canBeGrabbed; set => canBeGrabbed = value; }

		[SerializeField]
		private bool canBePickedUp = true;
		public bool CanBePickedUp { get => canBePickedUp; set => canBePickedUp = value; }
		
		[SerializeField]
		private bool canBeSwapped = true;
		public bool CanBeSwapped { get => canBeSwapped; set => canBeSwapped = value; }

		[SerializeField]
		private bool autoPickup = true;

		public bool AutoPickup { get => autoPickup; set => autoPickup = value; }

		public abstract void HandleItemGrabbed(GameplayHand hand);

		public abstract void HandleItemReleased(GameplayHand hand);
		public abstract void HandleGrabProcessStarted(GameplayHand gameplayHand);
		public abstract void UpdateInput(PlayerInputConsumerAccessService playerInput);
	}
}