using Game.Scripts.Core.Loading;
using Game.Scripts.Gameplay.Services;
using Quack.ReferenceMagic.Runtime;
using Quack.Utils;
using Sirenix.OdinInspector;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Gameplay.Movement
{
	public class ZombieMovementBehaviour : MonoBehaviour, ISceneInitializedHandler
	{
		[Inject]
		private GameplayPlayerAccessService playerAccessService;

		[SerializeField, Required, Find(Destination.Ancestors)]
		private ZombieRig zombieRig;

		[SerializeField]
		private float moveSpeed = 0.5f;

		private bool canMove;

		private void Awake()
		{
			SceneBroadcaster.RegisterReceiver(this);
		}

		private void Update()
		{
			if (!canMove) return;

			MoveZombie();
			RotateZombie();
		}

		private void MoveZombie()
		{
			var step = moveSpeed * Time.deltaTime;
			var playerPosition = playerAccessService.PlayerTransform.position;
			var root = zombieRig.transform;

			root.position = Vector2.MoveTowards(root.position, playerPosition, step);
		}

		private void RotateZombie()
		{
			var root = zombieRig.transform;
			var playerPosition = playerAccessService.PlayerTransform.position;

			var difference = new Vector3()
			{
				x = root.position.x - playerPosition.x,
				y = root.position.y - playerPosition.y
			};
			var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			root.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + -90f));
		}

		void ISceneInitializedHandler.HandleSceneInitialized()
		{
			canMove = true;
		}
	}
}