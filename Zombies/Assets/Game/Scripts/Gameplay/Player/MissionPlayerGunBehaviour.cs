using Game.Save;

namespace Game.Scripts.Gameplay.Player
{
    public class MissionPlayerGunBehaviour : AbstractPlayerWeaponBehaviour
    {
        public override void Init(ILoadout loadout)
        {
            base.Init(loadout);

            if (!loadout.HasPrimaryWeapon) return;

            PrimaryWeapon = CreateWeapon(loadout.PrimaryWeaponId);
            PrimaryWeapon.WeaponHoldable.CanBeSwapped = true;
            PrimaryHand.Grab(PrimaryWeapon.WeaponHoldable);
        }
    }
}