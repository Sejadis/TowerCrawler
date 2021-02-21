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
            items.ForEach(i => { Items.Add(new EquipmentStateSave(i as Equipment)); });
        }

        public List<Item> GetItems()
        {
            var items = new List<Item>();
            var resourceManager = ResourceManager.Instance;
            foreach (var itemSave in Items)
            {
                var item = resourceManager.GetEquipmentByID(itemSave.guid).SetValuesFromSave(itemSave);
                items.Add(item);
            }

            return items;
        }
    }
}