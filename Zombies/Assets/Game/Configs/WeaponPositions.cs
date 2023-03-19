using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
	public class WeaponPositions : MonoBehaviour
	{
		[SerializeField]
		private Transform pistolTransform;

		private Dictionary<int, Transform> weaponPositions = new();

		private void Awake()
		{
			// Make it so this can be generated on its own.
			weaponPositions.Add(1, pistolTransform);
		}

		public bool TryGetPosition(int weaponId, out Transform position)
		{
			position = null;
			if (weaponPositions.TryGetValue(weaponId, out var cachedPosition))
			{
				position = cachedPosition;
				return true;
			}

			return false;
		}
	}
}