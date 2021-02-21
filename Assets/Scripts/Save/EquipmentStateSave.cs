using System;
using System.Collections.Generic;
using SejDev.Systems.Equipment;

namespace SejDev.Save
{
    [Serializable]
    public class EquipmentStateSave
    {
        public readonly string guid;
        public readonly List<EquipmentStat> stats;

        public EquipmentStateSave(string guid, List<EquipmentStat> stats)
        {
            this.guid = guid;
            this.stats = stats;
        }

        // public EquipmentStateSave(Equipment equipment)
        // {
        //     guid = equipment.GUID;
        //     stats = equipment.stats;
        // }
    }
}