using System;
using System.Collections.Generic;
using SejDev.Save;
using SejDev.Systems.Stats;

namespace SejDev.Systems.Equipment
{
    public class EquipmentHolder
    {
        private readonly IInventory inventory;
        private readonly IStats statsManager;
        private readonly Dictionary<EquipSlotType, Equipment> equipment = new Dictionary<EquipSlotType, Equipment>();
        private readonly WeaponHandler weaponHandler;

        public EquipmentHolder(IStats statsManager, IInventory inventory, WeaponHandler weaponHandler)
        {
            this.statsManager = statsManager;
            this.inventory = inventory;
            this.weaponHandler = weaponHandler;
            var equipmentSave = SaveManager.GetSave<EquipmentSave>()?.GetEquipment();

            if (equipmentSave != null)
            {
                foreach (var key in equipmentSave.Keys)
                {
                    SetItem(equipmentSave[key]);
                }
            }

            if (equipment.TryGetValue(EquipSlotType.Weapon, out var weapon))
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

        private void SetItem(Equipment item)
        {
            equipment[item.EquipSlot] = item;
            item.stats.ForEach(stat => statsManager.AddModifier(stat.type, stat.modifier));
            if (item.EquipSlot == EquipSlotType.Weapon)
            {
                weaponHandler.EquipWeapon(item as Weapon);
            }
        }

        public void EquipItem(Equipment item)
        {
            if (!inventory.ContainsItem(item)) throw new InvalidOperationException("Item not in Inventory");

            var currentItem = GetItemForSlot(item.EquipSlot);
            if (currentItem != null)
            {
                inventory.AddItem(currentItem); //TODO fix switching gear with full inventory
                currentItem.stats.ForEach(stat => statsManager.RemoveModifier(stat.type, stat.modifier));
            }

            equipment[item.EquipSlot] = item;
            item.stats.ForEach(stat => statsManager.AddModifier(stat.type, stat.modifier));
            if (item.EquipSlot == EquipSlotType.Weapon)
            {
                weaponHandler.EquipWeapon(item as Weapon);
            }

            inventory.RemoveItem(item);
            var save = new EquipmentSave(equipment);
            SaveManager.SetSave(save);
        }

        public void UnEquipItem(Equipment item)
        {
            if (!equipment.ContainsValue(item)) throw new InvalidOperationException("Item not equipped");
            inventory.AddItem(item); //TODO fix switching gear with full inventory

            item.stats.ForEach(stat => statsManager.RemoveModifier(stat.type, stat.modifier));
            equipment.Remove(item.EquipSlot);
            if (item.EquipSlot == EquipSlotType.Weapon)
            {
                weaponHandler.UnEquipWeapon();
            }

            var save = new EquipmentSave(equipment);
            SaveManager.SetSave(save);
        }
    }
}