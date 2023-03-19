using Game.Save;
using Game.Scripts.Gameplay.Weapons;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
    public class MissionPlayerGunBehaviour : AbstractPlayerWeaponBehaviour
    {
        public override void Init(ILoadout loadout)
        {
            base.Init(loadout);

            if (!loadout.HasPrimaryWeapon) return;

            PrimaryWeapon = CreateWeapon(loadout.PrimaryWeaponId);
            Debug.Log($"Current weapon is {PrimaryWeapon.GetType()}");
            WeaponHoldable.SetCurrentWeapon(PrimaryWeapon as IUsableWeapon);
        }
    }
}