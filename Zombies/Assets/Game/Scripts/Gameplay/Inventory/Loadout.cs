using System.Collections.Generic;
using Game.Save;

namespace Game.Scripts.Gameplay.Inventory
{
    public class Loadout : ILoadout
    {
        public bool HasPrimaryWeapon { get; set; }
        public int PrimaryWeaponId { get; set; }
        public Inventory Inventory { get; set; }
    }

    public class Inventory
    {
        public Dictionary<int, InventorySlot> inventorySlots = new();
    }

    public class InventorySlot
    {
        public ItemObject item;
        public int amount;

        public InventorySlot(ItemObject item, int amount)
        {
            item = item;
            amount = amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }

    public interface IInventorySlot
    {
        int SlotId { get; }
        string Description { get; }
    }
}