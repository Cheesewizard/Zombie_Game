using Cysharp.Threading.Tasks;
using UnityDependencyInjection;
using UnityEngine;
using UnityEngine.InputSystem;
using Zombieland.Gameplay.Services;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponHoldable : MonoBehaviour
    {
        [Inject]
        private PlayerInputConsumerAccessService playerInput;

        public IUsableWeapon currentWeapon;
		
        private void Start()
        {
            playerInput.playerInput.Player.Shoot.performed += HandleShootGun;
        }

        public void SetCurrentWeapon(IUsableWeapon newWeapon)
        {
            currentWeapon = newWeapon;
        }
		
        private async void HandleShootGun(InputAction.CallbackContext context)
        {
            if (currentWeapon == null) return; // Do we always start with pistol?
            await currentWeapon.PerformAction();
        }

        private void OnDestroy()
        {
            playerInput.playerInput.Player.Shoot.performed -= HandleShootGun;
        }
    }

    public interface IUsableWeapon
    {
        public UniTask PerformAction();
    }
}