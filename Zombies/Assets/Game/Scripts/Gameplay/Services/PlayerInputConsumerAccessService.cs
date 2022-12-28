using Game.Scripts.Controls;

namespace Zombieland.Gameplay.Services
{
    public class PlayerInputConsumerAccessService : PlayerInputConsumerAccessService.IPlayerInputConsumerAccessService
    {
        public PlayerInput playerInput { get; }

        public PlayerInputConsumerAccessService()
        {
            playerInput = PlayerInputLocator.GetPlayerInput();
        }
        
        public interface IPlayerInputConsumerAccessService
        {
            PlayerInput playerInput { get; }
        }
    }
}