using System;
using System.Collections.Generic;
using SejDev.Systems.Equipment;

namespace SejDev.Save
{
    [Serializable]
    public class InventorySave : Systems.Save.Save
    {
        public InventorySave(List<EquipmentStateSave> items)
        {
            Items = items;
        }

        public List<EquipmentStateSave> Items { get; }

        public InventorySave(List<Item> items)
        {
            Items = new List<EquipmentStateSave>();
            items.ForEach(i =>
            {
                var eq = i as Equipment;
                Items.Add(new EquipmentStateSave(eq.GUID, eq.stats));
            });
        }

        public List<Item> GetItems()
        {
            var items = new List<Item>();
            var resourceManager = ResourceManager.Instance;
            foreach (var item in Items)
            {
                var i = resourceManager.GetEquipmentByID(item.guid);
                i.stats = item.stats;
                items.Add(i);
            }

            return items;
        }
    }
}