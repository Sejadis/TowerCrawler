using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SejDev.Systems.Gear
{
    public class Inventory : IInventory
    {
        private readonly List<Item> items;
        public int MaxSpace { get; }
        public Action OnInventoryChanged;

        public Inventory(int maxSpace)
        {
            MaxSpace = maxSpace;
        }

        public Inventory(int maxSpace, List<Item> items = null) : this(maxSpace)
        {
            this.items = items ?? new List<Item>();
        }

        public Inventory(int maxSpace, IEnumerable<string> itemIDs) : this(maxSpace)
        {
            items = new List<Item>();
            if (itemIDs == null) return;
            foreach (var id in itemIDs)
            {
                items.Add(ResourceManager.Instance.GetEquipmentByID(id));
            }
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
            OnInventoryChanged.Invoke();
        }

        public void RemoveItem(Item item)
        {
            if (!items.Contains(item)) throw new InvalidOperationException("Item not in Inventory");
            items.Remove(item);
            OnInventoryChanged.Invoke();
        }

        public bool ContainsItem(Item item)
        {
            return items.Contains(item);
        }
    }
}