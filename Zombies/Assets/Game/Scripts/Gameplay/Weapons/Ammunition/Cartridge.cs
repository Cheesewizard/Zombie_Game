using Game.Scripts.Gameplay.Guns;
using Game.Scripts.Utils;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Ammunition
{
	public class Cartridge : Ammo
	{
		[SerializeField, Required, Find(Destination.AllChildren)]
		private Bullet bullet;

		[SerializeField, Required, Find(Destination.Self)]
		private SpriteRenderer caseRenderer;
		
		[SerializeField, Required, Find(Destination.Self)]
		protected Rigidbody2D caseRigidbody;
		
		public override bool IsValid => bullet != null;
		public override SpriteRenderer CaseRenderer => caseRenderer;
		public override uint GetBulletId() => bullet.ID;
		public override uint GetDetonationId() => bullet.DetonationID;

		public bool IsLoaded;

		public override void Init()
		{
			base.Init();
			bullet.Init();
			IsLoaded = false;
		}

		public override void ResetTransform(Transform parent)
		{
			base.ResetTransform(parent);

			caseRigidbody.Attach(parent);

			// Init bullet and parent it to this transform
			bullet.ResetTransform(transform);
			IsLoaded = false;
		}

		public override void HandleDetonation(Gun gun)
		{
			bullet.Launch(gun, BulletHelper.NextDetonateID, HandleStopFlying);
		}
	}
}