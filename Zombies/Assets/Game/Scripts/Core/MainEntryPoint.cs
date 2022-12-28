using Game.Scripts.Gameplay.Guns;
using UnityEngine;

namespace Game.Scripts.Core
{
	public class MainEntryPoint : MonoBehaviour
	{
		private void Awake()
		{
			AutoStarter.IsGameRunningStandalone = false;
		}

		private async void Start()
		{
			await CoreScene.LoadAsync();

			await LandingPageScene.LoadAsync();
		}
	}
}