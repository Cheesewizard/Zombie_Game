using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Services;
using UnityDependencyInjection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponHoldable : MonoBehaviour, IWeaponHoldable, IDependencyInjectionCompleteHandler
    {
        [Inject]
        private PlayerInputConsumerAccessService playerInput;

        public IUsableWeapon currentWeapon;
        public bool CanBeSwapped { get; set; }

        public event Action onWeaponEquipped;

        public void HandleDependencyInjectionComplete()
        {
            playerInput.InputConsumer.Player.Shoot.performed += HandleShootGun;
        }

        public void SetCurrentWeapon(IUsableWeapon newWeapon)
        {
            currentWeapon = newWeapon;
            onWeaponEquipped?.Invoke();
        }

        private async void HandleShootGun(InputAction.CallbackContext context)
        {
            if (currentWeapon == null) return; // Do we always start with pistol?
            await currentWeapon.PerformAction();
        }

        private void OnDestroy()
        {
            playerInput.InputConsumer.Player.Shoot.performed -= HandleShootGun;
        }
    }

    public interface IUsableWeapon
    {
        public int WeaponId { get; }
        public UniTask PerformAction();
    }

    public interface IWeaponHoldable
    {
        public bool CanBeSwapped { get; set;}
    }
}