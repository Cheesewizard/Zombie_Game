using Game.Scripts.Core.Loading;
using Quack.Utils;
using UnityEngine;

namespace Game.Scripts.Core
{
	public class LandingPageEntryPoint : MonoBehaviour, ILoadingScreenHiddenHandler
	{
		private void Awake()
		{
			SceneBroadcaster.RegisterReceiver(this);
		}

		public async void HandleLoadingScreenHidden()
		{
			await MissionScene.LoadAsync();
		}
	}
}