using System;
using System.Collections.Generic;
using Game.Configs;
using Game.Scripts.Gameplay.Services;
using Game.Scripts.Gameplay.Weapons.Magazines;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public class PlayerBelt : MonoBehaviour
	{
		[Inject]
		private readonly ILoadoutService loadoutService = null;

		[SerializeField, Required, Find(Destination.AllChildren)]
		private MagazineReloader magazineReloaderTemplate;

		private readonly Dictionary<int, MagazineReloader> magazineReloaders = new();

		private void Awake()
		{
			magazineReloaderTemplate.gameObject.SetActive(false);
		}
		
		public MagazineReloader CreateReloader(string gunName, int gunId, MagazineConfig magazineConfig, Transform gunTransform)
		{
			if (magazineReloaders.ContainsKey(gunId))
			{
				throw new Exception("A reloader already exists for this gun");
			}

			var reloader = Instantiate(magazineReloaderTemplate, magazineReloaderTemplate.transform.parent);
			reloader.gameObject.SetActive(true);
			magazineReloaders.Add(gunId, reloader);

			reloader.Init(gunName, gunId, magazineConfig, gunTransform);
			return reloader;
		}

	}
}