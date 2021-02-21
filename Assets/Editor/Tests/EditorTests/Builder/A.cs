using SejDev.Systems.Equipment;

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

        public static HealthManagerBuilder HealthManager()
        {
            return new HealthManagerBuilder();
        }
    }
}