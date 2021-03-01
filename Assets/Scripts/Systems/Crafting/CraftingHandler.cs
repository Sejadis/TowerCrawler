using System;
using System.Linq;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SejDev.Systems.Crafting
{
    [RequireComponent(typeof(IInventory))]
    public class CraftingHandler : MonoBehaviour
    {
        private IInventory inventory;

        private void Start()
        {
            inventory = GetComponent<IInventory>();
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
                    .Where(slotType => result.possibleResults.HasFlag(slotType)).ToArray();
                var rolledSlot = filteredValue[Random.Range(0, filteredValue.Length)];
                var possibleItems = ResourceManager.Instance.GetItemsForSlot(rolledSlot);
                var selectedItem = Utility.GetRandomValueFromList(possibleItems);
                rolledItem = StatRollManager.RollStats(selectedItem, result.rarity);
            }

            item = rolledItem;
            return item != null;
        }
    }
}