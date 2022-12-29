using Quack.Utils;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class BulletTarget : AbstractEventDispatcherBehaviour<BulletTarget.IBulletHitReceiver>
	{
		[SerializeField]
		private bool isImpenetrable;
		public bool IsImpenetrable => isImpenetrable;

		public interface IBulletHitReceiver
		{
			void HandleBulletHit(Bullet bullet, RaycastHit2D hit);
		}

		public void NotifyBulletHit(Bullet bullet, RaycastHit2D hit)
		{
			for (var i = receivers.Count - 1; i >= 0; --i)
			{
				receivers[i].HandleBulletHit(bullet, hit);
			}
		}
	}
}