using UnityDependencyInjection;
using Zombieland.Gameplay.Services;

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

            var inputConsumerAccessService = DependencyContainer
                .GetDependency<PlayerInputConsumerAccessService.IPlayerInputConsumerAccessService>();
            
            inputConsumerAccessService.playerInput.Enable();
        }

        public void Start()
        {
            //playerInputConsumer.SetEnabled(true);
        }

        private void Destroy()
        {
            DependencyContainer.Destroy();
        }

        private void AddDefaultServices()
        {
            // Add default service here
            DependencyContainer.Add(new PlayerInputConsumerAccessService());

            switch (currentGameMode)
            {
                case GameMode.Game:
                    break;
            }
        }
    }
}