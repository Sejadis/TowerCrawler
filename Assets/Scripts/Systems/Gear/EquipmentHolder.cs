using System;

namespace SejDev.Systems.Gear
{
    public class EquipmentHolder
    {
        private readonly IInventory inventory;
        private Equipment weaponSlot;

        public EquipmentHolder(IInventory inventory, Equipment weaponSlot = null)
        {
            this.inventory = inventory;
            this.weaponSlot = weaponSlot;
        }

        public Equipment GetItemForSlot(EquipSlotType equipSlot)
        {
            Equipment item = null;
            switch (equipSlot)
            {
                case EquipSlotType.Weapon:
                {
                    item = weaponSlot;
                    break;
                }
            }

            return item;
        }

        public void EquipItem(Equipment item)
        {
            if (!inventory.ContainsItem(item)) throw new InvalidOperationException("Item not in Inventory");

            var currentItem = GetItemForSlot(item.EquipSlot);
            if (currentItem != null) inventory.AddItem(currentItem);

            switch (item.EquipSlot)
            {
                case EquipSlotType.Weapon:
                {
                    weaponSlot = item;
                    break;
                }
            }

            inventory.RemoveItem(item);
        }
    }
}