using System;
using System.Collections.Generic;
using System.Linq;
using SejDev.Player;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.Systems.Crafting
{
    public class CraftingHandler : MonoBehaviour
    {
        private IInventory inventory;

        private void Start()
        {
            inventory = GetComponent<PlayerInventory>().Inventory;
        }

        public bool TryCraft(CraftingBlueprint blueprint, out Item item)
        {
            foreach (var craftingCost in blueprint.CraftingCosts)
            {
                if (!inventory.ContainsCurrency(craftingCost.currencyData, craftingCost.amount))
                {
                    //at least one currency is missing
                    item = null;
                    return false;
                }
            }
            //if we are here we have enough currency to continue

            Item rolledItem = null;
            var result = blueprint.CraftingResult;
            if (result.item != null)
            {
                rolledItem = StatRollManager.RollStats(result.item as Equipment.Equipment, result.rarity);
            }
            else
            {
                var enumValues = Enum.GetValues(typeof(EquipSlotType));
                var filteredValue = enumValues.Cast<EquipSlotType>()
                    .Where(slotType => result.possibleResults.HasFlag(slotType)).ToList();
                EquipSlotType rolledSlot;
                List<Equipment.Equipment> possibleItems;
                int maxTries = 5; //TODO maybe throw error instead of retrieing?
                do
                {
                    rolledSlot = Utility.GetRandomValueFromList(filteredValue);
                    possibleItems = ResourceManager.Instance.GetItemsForSlot(rolledSlot);
                } while (possibleItems.Count == 0 && maxTries-- > 0);

                var selectedItem = Utility.GetRandomValueFromList(possibleItems);
                rolledItem = StatRollManager.RollStats(selectedItem, result.rarity);
            }

            foreach (var craftingCost in blueprint.CraftingCosts)
            {
                inventory.RemoveCurrency(craftingCost.currencyData, craftingCost.amount);
            }

            item = rolledItem;
            return (bool) item;
        }
    }
}