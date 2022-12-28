using UnityDependencyInjection;
using UnityEngine.InputSystem;
using Zombieland.Gameplay.Services;

namespace Game.Scripts.Gameplay.Guns
{
    public class Pistol : Gun
    {
        [Inject]
        private PlayerInputConsumerAccessService playerInput;
        
        private void Start()
        {
            base.Init(gunConfig);
          //  playerInput.playerInput.Player.Shoot.performed += HandleShootGun;
        }

        private void HandleShootGun(InputAction.CallbackContext context)
        {
            base.Shoot();
        }

        private void OnDestroy()
        {
           // playerInput.playerInput.Player.Shoot.performed -= HandleShootGun;
        }
    }
}