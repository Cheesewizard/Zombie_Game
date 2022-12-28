using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Gameplay.Guns
{
    public class CoreScene : MonoBehaviour
    {
        private const string CORE_SCENE = "Assets/Game/Scenes/System/Core.unity";
        private static CoreScene instance;

        private void Awake()
        {
            instance = this;
        }

        public static async UniTask LoadAsync()
        {
            await SceneManager.LoadSceneAsync(CORE_SCENE, LoadSceneMode.Additive);

            await UniTask.WaitUntil(() => instance != null);

            await instance.Init();
        }

        private UniTask Init()
        {
            // await SaveGameData.Instance.LoadOrCreateNewGame(contentLibrary);
            //
            //
            // SceneBroadcaster.BroadcastEvent<ICoreSceneInitializedHandler>(
            //     t => t.HandleCoreSceneInitialized());

            var gameSession = new GameSession(GameMode.Game);
            gameSession.Init();

            return UniTask.CompletedTask;
            
        }
    }
}