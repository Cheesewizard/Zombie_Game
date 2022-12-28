using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Services;
using Quack.Utils;
using UnityEngine;
using Game.Scripts.Core.Loading;
using Game.Scripts.Gameplay;

namespace Game.Scripts.Core
{
	public class MissionEntryPoint : MonoBehaviour, ISceneInitializerAsync
	{
		[SerializeField]
		private Transform spawnPoint;

		private void Awake()
		{
			SceneBroadcaster.RegisterReceiver(this);
		}

		public async UniTask InitializeSceneAsync()
		{
			var playerRig = await PlayerRig.LoadAsync();
			playerRig.transform.position = spawnPoint.position;

			var gameSession = new GameSession(GameMode.Game);
			gameSession.DependencyContainer.Add(new PlayerAccessService(playerRig));
			gameSession.Init();

			gameSession.DependencyContainer.InjectToSceneObjects();
		}
	}
}