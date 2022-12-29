using UnityEngine;

namespace Game.Configs
{
	public static class LayerMasks
	{
		public static readonly int BulletLayer;

		static LayerMasks()
		{
			BulletLayer = LayerMask.GetMask(
				Layers.DEFAULT,
				Layers.ENEMY,
				Layers.PLAYER,
				Layers.WALL);
		}
	}
}