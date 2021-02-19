using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SejDev.Systems.Gear
{
    public class Inventory : IInventory
    {
        private readonly List<Item> items;
        public int MaxSpace { get; }

        public Inventory(int maxSpace, List<Item> items = null)
        {
            this.items = items ?? new List<Item>();
            MaxSpace = maxSpace;
        }

        public Inventory() : this(10)
        {
        }

        public int AvailableSpace => MaxSpace - items.Count;
        public ReadOnlyCollection<Item> Items => items.AsReadOnly();

        public void AddItem(Item item)
        {
            if (AvailableSpace <= 0) throw new InvalidOperationException("Inventory full");
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if (!items.Contains(item)) throw new InvalidOperationException("Item not in Inventory");
            items.Remove(item);
        }

        public bool ContainsItem(Item item)
        {
            return items.Contains(item);
        }
    }
}