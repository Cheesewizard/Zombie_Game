using UnityDependencyInjection;

namespace Game.Scripts.Gameplay.Services
{
    public enum GameMode
    {
        Game,
    }

    /// <summary>
    /// Represents a "session" in the game, (IE: a level, or a visit to the hub)
    /// </summary>
    public class GameSession
    {
        public DependencyContainer DependencyContainer { get; }

        private readonly GameMode currentGameMode;

        public GameSession(GameMode gameMode)
        {
            currentGameMode = gameMode;
            DependencyContainer = new DependencyContainer();
        }

        public void Init()
        {
            AddDefaultServices();
            DependencyContainer.SelfInject();

            var inputConsumerAccessService = DependencyContainer.GetDependency<IPlayerInputConsumerAccessService>();
            inputConsumerAccessService.InputConsumer.Enable();
        }

        private void Destroy()
        {
            DependencyContainer.Destroy();
        }

        private void AddDefaultServices()
        {
            // Add default services here
            DependencyContainer.Add(new PlayerInputConsumerAccessService());

            switch (currentGameMode)
            {
                case GameMode.Game:
                    break;
            }
        }
    }
}