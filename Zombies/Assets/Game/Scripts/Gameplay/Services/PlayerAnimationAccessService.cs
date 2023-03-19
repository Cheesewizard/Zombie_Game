using Game.Scripts.Gameplay.Player;

namespace Game.Scripts.Gameplay.Services
{
	public class PlayerAnimationAccessService : IPlayerAnimationAccessService
	{
		public UnityEngine.Animator PlayerAnimator { get; }

		public PlayerAnimationAccessService(PlayerRig playerRig)
		{
			PlayerAnimator = playerRig.PlayerAnimator;
		}
	}

	public interface IPlayerAnimationAccessService
	{
		public UnityEngine.Animator PlayerAnimator { get; }
	}
}