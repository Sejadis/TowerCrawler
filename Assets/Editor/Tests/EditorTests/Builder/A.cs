using SejDev.Systems.Gear;
using SejDev.Systems.Skills;

namespace Editor.Tests.EditorTests.Builder
{
    public static class A
    {
        public static InventoryBuilder Inventory()
        {
            return new InventoryBuilder();
        }
        public static EquipmentHolderBuilder EquipmentHolder(IInventory inventory)
        {
            return new EquipmentHolderBuilder(inventory);
        }

        public static EquipmentBuilder Equipment()
        {
            return new EquipmentBuilder();
        }

        public static SkillHandlerBuilder SkillHandler(ISkillEffect skillEffect)
        {
            return new SkillHandlerBuilder(skillEffect);
        }

        public static HealthManagerBuilder HealthManager()
        {
            return new HealthManagerBuilder();
        }
    }
}