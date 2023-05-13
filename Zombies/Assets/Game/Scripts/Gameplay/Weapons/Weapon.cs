using System;
using Game.Configs;
using Game.Scripts.Gameplay.Player;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Self)]
        private WeaponHoldable weaponHoldable;
        public WeaponHoldable WeaponHoldable => weaponHoldable;
        
        // Events
        public event Action<WeaponConfig> OnUseWeapon;
        public event Action OnFinishUseWeapon;
        public event Action<Weapon> OnActivated;
        public event Action<Weapon> OnDeactivated;

        public abstract int WeaponId { get; }

        protected abstract void Activate();
        protected abstract void Deactivate();

        public abstract void Init(PlayerBelt belt);

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