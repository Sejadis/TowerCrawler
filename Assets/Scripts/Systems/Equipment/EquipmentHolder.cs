using System;
using System.Collections.Generic;
using SejDev.Save;

namespace SejDev.Systems.Equipment
{
    public class EquipmentHolder
    {
        private readonly IInventory inventory;
        private readonly Dictionary<EquipSlotType, Equipment> equipment;
        private readonly WeaponHandler weaponHandler;

        public EquipmentHolder(IInventory inventory, WeaponHandler weaponHandler)
        {
            this.inventory = inventory;
            this.weaponHandler = weaponHandler;
            var equipmentSave = SaveManager.GetSave<EquipmentSave>();
            equipment = equipmentSave?.GetEquipment() ?? new Dictionary<EquipSlotType, Equipment>();
            if (equipment != null && equipment.TryGetValue(EquipSlotType.Weapon, out var weapon))
            {
                weaponHandler.EquipWeapon(weapon as Weapon);
            }
        }

        public Equipment GetItemForSlot(EquipSlotType equipSlot)
        {
            if (equipment == null) return null;
            equipment.TryGetValue(equipSlot, out var eq);
            return eq;
        }

        public void EquipItem(Equipment item)
        {
            if (!inventory.ContainsItem(item)) throw new InvalidOperationException("Item not in Inventory");

            var currentItem = GetItemForSlot(item.EquipSlot);
            if (currentItem != null) inventory.AddItem(currentItem); //TODO fix switching gear with full inventory

            equipment[item.EquipSlot] = item;
            if (item.EquipSlot == EquipSlotType.Weapon)
            {
                weaponHandler.EquipWeapon(item as Weapon);
            }

            inventory.RemoveItem(item);
            var save = new EquipmentSave(equipment);
            SaveManager.SetSave(save);
        }
    }
}