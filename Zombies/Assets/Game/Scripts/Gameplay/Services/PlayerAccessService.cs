using UnityEngine;

namespace Game.Scripts.Gameplay.Services
{
	public class PlayerAccessService : IPlayerTransformAccessService
	{
		public PlayerRig PlayerRig { get; }

		public Transform PlayerTransform => PlayerRig.transform;

		public Camera PlayerCamera { get; }

		public PlayerAccessService(PlayerRig playerRig)
		{
			PlayerRig = playerRig;
			PlayerCamera = PlayerRig.GetComponentInChildren<Camera>();
		}
	}
}