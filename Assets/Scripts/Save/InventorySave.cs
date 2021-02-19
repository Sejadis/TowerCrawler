using System;
using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Gear;

namespace SejDev.Save
{
    [Serializable]
    public class InventorySave : Systems.Save.Save
    {
        public InventorySave(List<Item> items)
        {
            Items = items.Select(i => i.GUID).ToList();
        }

        public List<string> Items { get; set; }
    }
}