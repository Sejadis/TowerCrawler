using System;
using System.Collections.Generic;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;

namespace SejDev.Save
{
    [Serializable]
    public class EquipmentStateSave
    {
        public readonly string guid;
        public readonly Rarity rarity;
        public readonly List<EquipmentStat> stats;

        public EquipmentStateSave(string guid, List<EquipmentStat> stats, Rarity rarity)
        {
            this.guid = guid;
            this.stats = stats;
            this.rarity = rarity;
        }

        public EquipmentStateSave(Equipment equipment)
        {
            guid = equipment.GUID;
            stats = equipment.stats;
            rarity = equipment.rarity;
        }
    }
}