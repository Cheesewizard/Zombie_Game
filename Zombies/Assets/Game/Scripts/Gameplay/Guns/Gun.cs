using System;
using Game.Scripts.Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Gameplay.Guns
{
    public class Gun : MonoBehaviour
    {
        private PlayerInput playerInput;

        public Action OnShoot;
        
        private void Start()
        {
            playerInput = PlayerInputLocator.GetPlayerInput();
            playerInput.Player.Shoot.performed += Shoot;
        }
        
        private void Shoot(InputAction.CallbackContext context)
        {
            OnShoot?.Invoke();
        }
        
        public virtual void Reload()
        {
            
        }

        private void OnDestroy()
        {
            playerInput.Player.Shoot.performed -= Shoot;
        }
    }
    
}
