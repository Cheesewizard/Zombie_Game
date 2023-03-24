using System;
using Game.Scripts.Gameplay.Weapons.Magazines;
using UnityEngine;

namespace Game.Scripts.Utils
{
	[Serializable]
	public class MagazineReference
	{
		[SerializeField]
		private string serializedResourcesPath;

		private Magazine cachedMagazine;

		public Magazine Magazine
		{
			get
			{
				if (cachedMagazine == null)
				{
					cachedMagazine = Resources.Load<Magazine>(serializedResourcesPath);
				}

				return cachedMagazine;
			}
		}
	}
}