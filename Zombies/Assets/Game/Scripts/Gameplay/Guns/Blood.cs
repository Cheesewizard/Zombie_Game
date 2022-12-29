using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class Blood : MonoBehaviour, BulletTarget.IBulletHitReceiver
	{
		[SerializeField, Required, Find(Destination.Siblings)]
		private ParticleSystem blood;

		void BulletTarget.IBulletHitReceiver.HandleBulletHit(Bullet bullet, RaycastHit2D hit)
		{
			if (blood == null) return;
			blood.Play();
		}
	}
}