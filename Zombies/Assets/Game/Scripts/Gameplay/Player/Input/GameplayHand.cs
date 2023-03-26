using System;
using Game.Scripts.Gameplay.Services;
using Sirenix.OdinInspector;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player.Input
{
	public class GameplayHand : MonoBehaviour
	{
		[Inject]
		private PlayerInputConsumerAccessService playerInput;

		public IHoldableItem CurrentHeldItem { get; private set; }

		// the current item we are grabbing (in the process of grabbing, not fully grabbed yet)
		private IHoldableItem currentGrabbingItem;

		public event Action<IHoldableItem> OnGrabbed;
		public event Action<IHoldableItem> OnReleased;

		private void Update()
		{
			if (currentGrabbingItem != null)
			{
				var item = currentGrabbingItem as UnityEngine.Object;
				if (item == null)
				{
					currentGrabbingItem = null;
				}
				else
				{
					HandleGrabProcessComplete();
					return;
				}
			}
			
			if (playerInput != null)
			{
				CurrentHeldItem?.UpdateInput(playerInput);
			}
		}

		[Button]
		public void Grab(IHoldableItem item)
		{
			// Check args
			if (item == null) throw new ArgumentNullException(nameof(item));
			if (item as UnityEngine.Object == null) throw new ArgumentNullException(nameof(item));
			if (!item.CanBeGrabbed) return;
			if (currentGrabbingItem != null) return;

			// notify process started
			item.HandleGrabProcessStarted(this);

			currentGrabbingItem = item;
		}

		private void HandleGrabProcessComplete()
		{
			// Set the current held item
			CurrentHeldItem = currentGrabbingItem;
			Debug.Log($"Current grabbed item is {CurrentHeldItem.GetType()}");
			
			// Notify grabbing complete
			CurrentHeldItem.HandleItemGrabbed(this);

			// We are no longer in the process of "grabbing" this item, but it's now "held"
			currentGrabbingItem = null;

			// Fire off event
			OnGrabbed?.Invoke(CurrentHeldItem);
		}

		[Button]
		public void Release()
		{
			if (CurrentHeldItem == null) return;

			var releasedItem = CurrentHeldItem;

			// Notify item released and clear the variable
			CurrentHeldItem = null;
			releasedItem.HandleItemReleased(this);

			// Fire off event
			OnReleased?.Invoke(releasedItem);
		}
	}
}