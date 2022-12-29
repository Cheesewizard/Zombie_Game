using Game.Scripts.Gameplay.Guns;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Effects
{
	public class BloodEffectBehaviour : MonoBehaviour, BulletTarget.IBulletHitReceiver
	{
		[SerializeField, Required, Find(Destination.Self)]
		private ParticleSystem bloodEffect;

		void BulletTarget.IBulletHitReceiver.HandleBulletHit(Bullet bullet, RaycastHit2D hit)
		{
			if (bloodEffect == null) return;

			// var bloodPosition = bloodEffect.transform.position;
			// bloodPosition = hit.point;
			//
			// var difference = new Vector3()
			// {
			// 	x = hit.point.x - bloodPosition.x,
			// 	y = hit.point.y - bloodPosition.y
			// };
			//
			// var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			// bloodEffect.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
			bloodEffect.Play();
		}
	}
}