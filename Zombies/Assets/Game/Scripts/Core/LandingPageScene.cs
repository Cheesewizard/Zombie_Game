using Cysharp.Threading.Tasks;
using Game.Scripts.Core.Loading;

namespace Game.Scripts.Core
{
	public class LandingPageScene
	{
		public static async UniTask LoadAsync()
		{
			var sceneLoader = new SceneLoader();
			await sceneLoader.Load(CoreScene.ContentLibrary.LandingPageSceneConfig);
		}
	}
}