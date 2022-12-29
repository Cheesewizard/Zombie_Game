using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
	public class ZombieRig : MonoBehaviour
	{
		[SerializeField, Required, Find(Destination.AllChildren)]
		private ZombieLogic zombieLogic;

		public ZombieLogic ZombieLogic => zombieLogic;
	}
}