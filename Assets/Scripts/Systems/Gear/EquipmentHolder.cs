using System;

namespace SejDev.Systems.Gear
{
    public class EquipmentHolder
    {
        private Equipment weaponSlot;
        private IInventory inventory;

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
                default:
                    break;
            }
            return item;
        }

        public void EquipItem(Equipment item)
        {
            if (!inventory.ContainsItem(item))
            {
                throw new InvalidOperationException("Item not in Inventory");
            }

            Equipment currentItem = GetItemForSlot(item.EquipSlot);
            if(currentItem != null){
                inventory.AddItem(currentItem);
            }

            switch (item.EquipSlot)
            {
                case EquipSlotType.Weapon:
                {
                    weaponSlot = item;
                    break;
                }
                default:
                    break;
            }
            inventory.RemoveItem(item);
        }
    }
}
