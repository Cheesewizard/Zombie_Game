using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Weapons;

namespace Game.Scripts.Gameplay.Guns
{
	public class Pistol : Gun, IUsableWeapon
	{
		private void Start()
		{
			WeaponHoldable.SetCurrentWeapon(this);
			base.Init(gunConfig);
		}

		public async UniTask PerformAction()
		{
			await base.Shoot();
		}
	}
}