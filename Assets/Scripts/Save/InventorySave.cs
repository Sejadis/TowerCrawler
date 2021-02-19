using System;
using System.Collections.Generic;
using SejDev.Systems.Gear;

namespace SejDev.Save
{
    [Serializable]
    public class InventorySave : Systems.Save.Save
    {
        public List<Item> Items { get; set; }
    }
}