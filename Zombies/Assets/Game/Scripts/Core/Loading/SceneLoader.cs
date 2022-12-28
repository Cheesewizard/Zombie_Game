using System;
using Cysharp.Threading.Tasks;
using Quack.SceneMagic;
using Quack.Utils;

namespace Game.Scripts.Core.Loading
{
	public class SceneLoader
	{
		public Func<UniTask> SceneInitializationHandlerAsync { private get; set; }
		public Action SceneInitializationHandler { private get; set; }

		public async UniTask Load(SceneConfig sceneConfig)
		{
			await sceneConfig.LoadScenes();

			// Notify new scene objects that scene is initialized.
			SceneBroadcaster.BroadcastEvent<ISceneInitializedHandler>(t => t.HandleSceneInitialized());

			// perform initializations (2 options here. sync or async)
			if (SceneInitializationHandlerAsync != null)
			{
				await SceneInitializationHandlerAsync();
			}

			SceneInitializationHandler?.Invoke();

			// Stops the load screen disappearing before the level has fully loaded
			await UniTask.NextFrame();

			// Notify new scene objects that loading screen is hidden
			SceneBroadcaster.BroadcastEvent<ILoadingScreenHiddenHandler>(t => t.HandleLoadingScreenHidden());
		}
	}
}