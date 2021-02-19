using System;
using System.Collections.Generic;
using SejDev.Systems.Gear;

namespace SejDev.Save
{
    [Serializable]
    public class EquipmentSave : Systems.Save.Save
    {
        private Dictionary<EquipSlotType, string> equipment = new Dictionary<EquipSlotType, string>();

        public EquipmentSave(Dictionary<EquipSlotType, Equipment> equipment)
        {
            foreach (EquipSlotType slotType in (EquipSlotType[]) Enum.GetValues(typeof(EquipSlotType)))
            {
                equipment.TryGetValue(slotType, out var eq);
                this.equipment[slotType] = eq?.GUID;
            }
        }

        public Dictionary<EquipSlotType, Equipment> GetEquipment()
        {
            var resourceManager = ResourceManager.Instance;
            var eq = new Dictionary<EquipSlotType, Equipment>();
            foreach (EquipSlotType slotType in (EquipSlotType[]) Enum.GetValues(typeof(EquipSlotType)))
            {
                eq[slotType] = resourceManager.GetEquipmentByID(equipment[slotType]);
            }

            return eq;
        }
    }
}