using System;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Scripts.Gameplay.Services;
using Quack.Utils;
using UnityEngine;
using Game.Scripts.Core.Loading;
using Game.Scripts.Gameplay.Inventory;
using Game.Scripts.Gameplay.Player;

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
			try
			{
				var playerRig = await PlayerRig.LoadAsync();
				playerRig.transform.position = spawnPoint.position;

				var gameSession = new GameSession(GameMode.Game);

				// Inject temporary loadout - should be determined from a save file
				var loadout = new Loadout()
				{
					PrimaryWeaponId = WeaponIDs.PISTOL,
					HasPrimaryWeapon = true,
					Inventory = null
				};

				playerRig.WeaponBehaviour.Init(loadout);
				gameSession.DependencyContainer.Add(new GameplayPlayerAccessService(playerRig));
				gameSession.DependencyContainer.Add(new PlayerAnimationAccessService(playerRig));
				gameSession.DependencyContainer.Add(new BulletImpactService());
				gameSession.DependencyContainer.Add(new LoadoutService(loadout));
				gameSession.Init();

				gameSession.DependencyContainer.InjectToSceneObjects();
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
				Debug.LogError(e.StackTrace);
			}
		}
	}
}