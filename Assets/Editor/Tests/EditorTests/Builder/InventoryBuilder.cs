using System.Collections.Generic;
using SejDev.Systems.Equipment;

namespace Editor.Tests.EditorTests.Builder
{
    public class InventoryBuilder
    {
        int maxSpace;
        List<Item> items;

        public InventoryBuilder(int maxSpace, List<Item> items)
        {
            this.maxSpace = maxSpace;
            this.items = items;
        }

        public InventoryBuilder() : this(5, new List<Item>())
        {
        }

        public InventoryBuilder WithMaxSpace(int maxSpace)
        {
            this.maxSpace = maxSpace;
            return this;
        }

        public InventoryBuilder WithItems(List<Item> items)
        {
            this.items = items;
            return this;
        }

        public Inventory Build()
        {
            return new Inventory(maxSpace, items);
        }

        public static implicit operator Inventory(InventoryBuilder inventoryBuilder)
        {
            return inventoryBuilder.Build();
        }
    }
}