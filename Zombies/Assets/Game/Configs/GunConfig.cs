using System;
using Game.Scripts.Gameplay.Guns;
using UnityEngine;

namespace Game.Configs
{
	[Serializable]
	public class GunConfig
	{
		[SerializeField]
		public GunTypes GunType;

		[SerializeField]
		public float FireRate;

		[SerializeField]
		public float BulletSpeed;

		[SerializeField]
		public float EffectiveRange;

		[SerializeField]
		public Transform SpawnPosition;
	}
}