using Game.Configs;
using Game.Scripts.Gameplay.Weapons.Magazines.Ammunition;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class MagazineReloader : MonoBehaviour
	{
		private MagazineStack magazineStack;
		public Magazine TakeMagazine() => magazineStack.TakeMagazine();

		public void AddAmmo(int amount) => magazineStack.AddAmmo(amount);

		public void Init(MagazineConfig magazineConfig)
		{
			magazineStack = new MagazineStack(magazineConfig);
		}
	}
}