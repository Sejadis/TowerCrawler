using System;
using SejDev.Systems.Stats;

namespace SejDev.Systems.Equipment
{
    [Serializable]
    public class EquipmentStat
    {
        public EquipmentStat(StatType type, Modifier modifier)
        {
            this.type = type;
            this.modifier = modifier;
        }

        public StatType type { get; }
        public Modifier modifier { get; }

        public override string ToString()
        {
            return
                $"+ {(modifier.value * (modifier.type == ModifierType.Percent ? 100 : 1)).ToString()}{(modifier.type == ModifierType.Percent ? "%" : "")} {Enum.GetName(typeof(StatType), type)}";
        }
    }
}