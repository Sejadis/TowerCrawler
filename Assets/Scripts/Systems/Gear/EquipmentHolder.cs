using System;

namespace SejDev.Systems.Gear
{
    public class EquipmentHolder
    {
        private readonly IInventory inventory;
        public Weapon weaponSlot;

        public EquipmentHolder(IInventory inventory, Weapon weaponSlot = null)
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
                    weaponSlot = item as Weapon;
                    break;
                }
            }

            inventory.RemoveItem(item);
        }
    }
}