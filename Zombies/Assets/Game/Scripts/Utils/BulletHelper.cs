namespace Game.Scripts.Utils
{
	public static class BulletHelper
	{
		/// <summary>
		/// The global bullet tick.
		/// Every time it assigned to something, the value increases.
		/// </summary>
		private static uint globalBulletIDCounter;

		// ++ is faster and skips the 0 (the default value, which will be ignored by targets).
		public static uint NextBulletID => ++globalBulletIDCounter;

		/// <summary>
		/// The global detonation tick.
		/// This is used to designate the bullet to a specific fire call.a
		/// </summary>
		private static uint globalDetonateIDCounter;

		public static uint NextDetonateID => ++globalDetonateIDCounter;
	}
}