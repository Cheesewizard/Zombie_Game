using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject, IInventorySlot
    {
        public List<InventorySlot> container = new List<InventorySlot>();

        public void AddItem(ItemObject item, int amount)
        {
            bool hasItem = false;
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item == item)
                {
                    container[i].AddAmount(amount);
                    hasItem = true;
                    break;
                }
            }

            if (!hasItem)
            {
                container.Add(new InventorySlot(item, amount));
            }
        }

        public int SlotId { get; }
        public string Description { get; }
    }
}