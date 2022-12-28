using Cysharp.Threading.Tasks;
using Game.Config;
using Game.Scripts.Core.Loading;
using Quack.ReferenceMagic.Runtime;
using Quack.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core
{
    public class CoreScene : MonoBehaviour
    {
        [SerializeField, Required, FindInAssets]
        private ContentLibrary contentLibrary;
        public static ContentLibrary ContentLibrary => instance.contentLibrary;

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

            SceneBroadcaster.BroadcastEvent<ICoreSceneInitializedHandler>(
                t => t.HandleCoreSceneInitialized());

            return UniTask.CompletedTask;
        }
    }
}