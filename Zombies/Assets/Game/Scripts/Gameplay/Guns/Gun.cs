using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Scripts.Gameplay.Weapons;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;

namespace Game.Scripts.Gameplay.Guns
{
	public abstract class Gun : Weapon
	{
		[SerializeField]
		protected GunConfig gunConfig;

		public GunConfig GunConfig => gunConfig;

		[SerializeField, Required, Find(Destination.Self)]
		protected Bullet bullet;

		[SerializeField]
		private Transform bulletSpawnPosition;

		public Transform BulletSpawnPosition => bulletSpawnPosition;

		public override int WeaponId => gunConfig.WeaponId;

		//public event Action<Ammo> OnAmmoFired;
		public event Action OnReload;
		public event Action OnEject;

		protected virtual void Init(GunConfig gunConfig)
		{
			this.gunConfig = gunConfig;
			//TODO Change this to be part of the pooling system
			bullet.Init();
		}

		protected abstract UniTask Fire();

		protected abstract void Reload();
	}
}