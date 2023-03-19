using Game.Scripts.Gameplay.Inventory;

namespace Game.Save
{
    public interface ILoadout
    {
        bool HasPrimaryWeapon { get; }
        int PrimaryWeaponId { get; }
        
        // PerkType Perk1 { get; }
        // PerkType Perk2 { get; }
        // PerkType Perk3 { get; }
        
        Inventory Inventory { get; set; }
    }
}