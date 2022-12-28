using Cysharp.Threading.Tasks;

namespace Game.Scripts.Core.Loading
{
	public interface ISceneInitializer
	{
		void InitializeScene();
	}

	public interface ISceneInitializerAsync
	{
		UniTask InitializeSceneAsync();
	}
}