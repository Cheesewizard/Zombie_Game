using UnityEngine;

namespace Game.Scripts.Gameplay.Services
{
	public interface IPlayerTransformAccessService
	{
		Transform PlayerTransform { get; }
	}

	// public interface IPlayerInputConsumerAccessService
	// {
	// 	InputConsumer InputConsumer { get; }
	// }
}