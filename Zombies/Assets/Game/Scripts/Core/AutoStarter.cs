using System.Linq;
using Game.Scripts.Core;
using Game.Scripts.Core.Loading;
using Quack.Utils;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
    public class AutoStarter : MonoBehaviour
    {
        public static bool IsGameRunningStandalone { get; set; } = true;

        private async void Start()
        {
            if (IsGameRunningStandalone)
            {
                IsGameRunningStandalone = false;
                Debug.Log("AutoStarter is taking over!");

                await CoreScene.LoadAsync();

                var asyncInitializer = FindObjectsOfType<MonoBehaviour>().OfType<ISceneInitializerAsync>().FirstOrDefault();
                if (asyncInitializer != null)
                {
                    await asyncInitializer.InitializeSceneAsync();
                }

                GetComponent<ISceneInitializer>()?.InitializeScene();

                SceneBroadcaster.BroadcastEvent<ISceneInitializedHandler>(t => t.HandleSceneInitialized());

                SceneBroadcaster.BroadcastEvent<ILoadingScreenHiddenHandler>(t => t.HandleLoadingScreenHidden());
            }
        }
    }

}