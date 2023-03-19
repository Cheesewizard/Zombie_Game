using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Gameplay.WorldObjects;
using Game.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Config
{
	[Serializable]
	public class BulletImpactDataLibrary
	{
		[FormerlySerializedAs("bulletImpactDataEntries")]
		[SerializeField]
		private WeaponImpactDataEntry[] weaponImpactDataEntries;

		private Dictionary<int, WeaponImpactDataEntry> weaponDataDictionary = new();

		public void Init()
		{
			weaponDataDictionary =
				weaponImpactDataEntries.ToDictionary(weaponData => weaponData.WeaponSource, bulletData => bulletData);

			foreach (var bulletImpactDataEntry in weaponImpactDataEntries)
			{
				bulletImpactDataEntry.Init();
			}
		}

		public bool TryGetWeaponImpactData(int gunId, out ImpactDataEntry[] impactData)
		{
			if (weaponDataDictionary.TryGetValue(gunId, out var weaponImpactData))
			{
				impactData = weaponImpactData.ImpactData;
				return true;
			}

			impactData = null;
			return false;
		}

		public bool TryGetMaxParticleCount(int gunId, out int maxParticleCount)
		{
			if (weaponDataDictionary.TryGetValue(gunId, out var weaponImpactData))
			{
				maxParticleCount = weaponImpactData.MaxParticleCount;
				return true;
			}

			maxParticleCount = 0;
			return false;
		}

		public bool TryGetMaxDecalCount(int weaponId, out int maxDecalCount)
		{
			if (weaponDataDictionary.TryGetValue(weaponId, out var weaponImpactData))
			{
				maxDecalCount = weaponImpactData.MaxDecalCount;
				return true;
			}

			maxDecalCount = 0;
			return false;
		}

		public bool TryGetSoundFX(int weaponId, ColliderSurfaceMaterial material, out AudioSource audioSource)
		{
			audioSource = null;
			if (weaponDataDictionary.TryGetValue(weaponId, out var weaponImpactData))
			{
				if (weaponImpactData.DataDictionary.TryGetValue(material, out var sound))
				{
					audioSource = sound.AudioSource;
					return true;
				}

				// If we can't find the sound for the specified material try return the default sound.
				if (weaponImpactData.DataDictionary.TryGetValue(ColliderSurfaceMaterial.Default, out var defaultImpactData))
				{
					audioSource = defaultImpactData.AudioSource;
					return true;
				}
			}

			return false;
		}
	}

	[Serializable]
	public class WeaponImpactDataEntry
	{
		[FormerlySerializedAs("bulletSource")]
		[SerializeField, ValueDropdown(OdinDropdowns.WEAPONS)]
		private int weaponSource;

		public int WeaponSource => weaponSource;

		[SerializeField]
		private int maxParticleCount;

		public int MaxParticleCount => maxParticleCount;

		[SerializeField]
		private int maxDecalCount;

		public int MaxDecalCount => maxDecalCount;

		[SerializeField]
		private ImpactDataEntry[] impactData;

		public ImpactDataEntry[] ImpactData => impactData;

		private Dictionary<ColliderSurfaceMaterial, ImpactDataEntry> dataDictionary = new();

		public Dictionary<ColliderSurfaceMaterial, ImpactDataEntry> DataDictionary => dataDictionary;

		public void Init()
		{
			dataDictionary = impactData.ToDictionary(impactData => impactData.Material, impactData => impactData);
		}
	}

	[Serializable]
	public class ImpactDataEntry
	{
		[SerializeField]
		private ColliderSurfaceMaterial material;

		public ColliderSurfaceMaterial Material => material;

		[FormerlySerializedAs("audioClip")]
		[SerializeField]
		private AudioSource audioSource;

		public AudioSource AudioSource => audioSource;

		[SerializeField]
		private ParticleSystem particleSystem;

		public ParticleSystem ParticleSystem => particleSystem;
	}
}