using System;
using System.Collections.Generic;
using SejDev.Systems.Equipment;

namespace SejDev.Save
{
    [Serializable]
    public class EquipmentSave : Systems.Save.Save
    {
        private Dictionary<EquipSlotType, EquipmentStateSave> equipment =
            new Dictionary<EquipSlotType, EquipmentStateSave>();

        public EquipmentSave(Dictionary<EquipSlotType, Equipment> equipment)
        {
            foreach (EquipSlotType slotType in (EquipSlotType[]) Enum.GetValues(typeof(EquipSlotType)))
            {
                if (equipment.TryGetValue(slotType, out var eq))
                {
                    this.equipment[slotType] = new EquipmentStateSave(eq);
                }
            }
        }

        public Dictionary<EquipSlotType, Equipment> GetEquipment()
        {
            var resourceManager = ResourceManager.Instance;
            var loadedEquipment = new Dictionary<EquipSlotType, Equipment>();
            foreach (EquipSlotType slotType in (EquipSlotType[]) Enum.GetValues(typeof(EquipSlotType)))
            {
                if (equipment.TryGetValue(slotType, out var eq))
                {
                    var equip = resourceManager.GetEquipmentByID(eq.guid);
                    equip.SetValuesFromSave(equipment[slotType]);
                    loadedEquipment[slotType] = equip;
                }
            }

            return loadedEquipment;
        }
    }
}