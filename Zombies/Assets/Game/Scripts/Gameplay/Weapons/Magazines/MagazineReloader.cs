using Game.Configs;
using Game.Scripts.Gameplay.Weapons.Magazines.Ammunition;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class MagazineReloader : MonoBehaviour
	{
		private MagazineStack magazineStack;
		public Magazine TakeMagazine() => magazineStack.TakeMagazine();
		
		// Fields
		private int gunId;
		private Transform gunTransform;
		
		public void AddAmmo(int amount) => magazineStack.AddAmmo(amount);

		public void Init(string gunName, int gunId, MagazineConfig magazineConfig, Transform gunTransform)
		{
			this.gunTransform = gunTransform;
			name = $"Reloader({gunName}:gunId-{gunId})";
			this.gunId = gunId;
			magazineStack = new MagazineStack(magazineConfig, transform);
		}

		public void Dispose()
		{
			magazineStack.Dispose();
			Destroy(gameObject);
		}
	}
}