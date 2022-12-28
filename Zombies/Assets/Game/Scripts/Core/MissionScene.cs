using Cysharp.Threading.Tasks;
using Game.Scripts.Core.Loading;
using UnityEngine;

namespace Game.Scripts.Core
{
	public class MissionScene
	{
		public static async UniTask LoadAsync()
		{
			var sceneLoader = new SceneLoader();
			sceneLoader.SceneInitializationHandlerAsync = Initialize;
			await sceneLoader.Load(CoreScene.ContentLibrary.MissionSceneConfig);
		}

		private static async UniTask Initialize()
		{
			await Object.FindObjectOfType<MissionEntryPoint>().InitializeSceneAsync();
		}
	}
}