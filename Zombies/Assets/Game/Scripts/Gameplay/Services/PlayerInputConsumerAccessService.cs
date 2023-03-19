using Game.Scripts.Controls;

namespace Game.Scripts.Gameplay.Services
{
    public class PlayerInputConsumerAccessService : IPlayerInputConsumerAccessService
    {
        public PlayerInput InputConsumer { get; }

        public PlayerInputConsumerAccessService()
        {
            InputConsumer = PlayerInputLocator.GetPlayerInput();
        }
    }

    public interface IPlayerInputConsumerAccessService
    {
        PlayerInput InputConsumer { get; }
    }
}