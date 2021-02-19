using SejDev.Systems.Gear;

namespace Editor.Tests.EditorTests.Builder
{
    public class EquipmentBuilder
    {
        EquipSlotType slotType;

        public EquipmentBuilder(EquipSlotType slotType)
        {
            this.slotType = slotType;
        }

        public EquipmentBuilder() : this(EquipSlotType.Weapon)
        {
        }

        public EquipmentBuilder WithSlotType(EquipSlotType slotType)
        {
            this.slotType = slotType;
            return this;
        }

        // public Equipment Build()
        // {
        //     //return new Equipment(slotType);
        // }

        // public static implicit operator Equipment(EquipmentBuilder equipmentBuilder)
        // {
        //     return equipmentBuilder.Build();
        // }
    }
}