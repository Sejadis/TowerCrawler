using System;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;

namespace SejDev.Systems.Crafting
{
    [Serializable]
    public class CraftingResult
    {
        public EquipSlotType possibleResults;
        public Rarity rarity; //TODO allow range 
        public Item item; //overrides possible results to guarantee that specific item
    }
}