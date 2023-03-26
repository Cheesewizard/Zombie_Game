using Game.Scripts.Gameplay.Player.Input;
using Game.Scripts.Gameplay.Services;

namespace Game.Scripts.Gameplay.Player
{
	public interface IHoldableItem
	{
		bool IsCurrentlyHeld { get; }
		
		bool CanBeGrabbed{ get; }
		bool CanBePickedUp { get; }
		
		bool CanBeSwapped { get; }
		
		bool AutoPickup { get; }

		void HandleItemGrabbed(GameplayHand hand);
		void HandleItemReleased(GameplayHand hand);
		void HandleGrabProcessStarted(GameplayHand gameplayHand);

		void UpdateInput(PlayerInputConsumerAccessService playerInput);
	}
}