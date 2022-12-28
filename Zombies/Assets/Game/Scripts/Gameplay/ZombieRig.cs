using System;
using Game.Scripts.Core.Loading;
using Quack.Utils;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
	public class ZombieRig : MonoBehaviour, ISceneInitializedHandler
	{
		private void Awake()
		{
			SceneBroadcaster.RegisterReceiver(this);
		}

		private void Start()
		{
			//gameObject.SetActive(false);
		}

		public void HandleSceneInitialized()
		{
			//gameObject.SetActive(true);
		}
	}
}