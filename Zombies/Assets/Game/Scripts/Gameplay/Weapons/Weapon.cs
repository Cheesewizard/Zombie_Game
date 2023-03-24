using System;
using Game.Configs;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        // Events
        public event Action<WeaponConfig> OnUseWeapon;
        public event Action OnFinishUseWeapon;
        public event Action<Weapon> OnActivated;
        public event Action<Weapon> OnDeactivated;

        public abstract int WeaponId { get; }

        protected abstract void Activate();
        protected abstract void Deactivate();

        public abstract void Init();

        protected virtual void RaiseUseWeaponEvent(WeaponConfig weaponConfig)
        {
            OnUseWeapon?.Invoke(weaponConfig);
        }

        protected virtual void RaiseFinishWeaponEvent()
        {
            OnFinishUseWeapon?.Invoke();
        }

        protected virtual void RaiseOnActivatedEvent(Weapon weapon)
        {
            OnActivated?.Invoke(weapon);
        }

        protected virtual void RaiseOnDeActivatedEvent(Weapon weapon)
        {
            OnDeactivated?.Invoke(weapon);
        }
    }
}