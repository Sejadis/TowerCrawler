using System;
using System.Collections.Generic;
using SejDev.Systems.Equipment;

namespace SejDev.Save
{
    [Serializable]
    public class InventorySave : Systems.Save.Save
    {
        public InventorySave(List<EquipmentStateSave> itemSaves, List<CurrencySave> currencieses)
        {
            ItemSaves = itemSaves;
            CurrencySaves = currencieses;
        }

        public List<EquipmentStateSave> ItemSaves { get; }
        public List<CurrencySave> CurrencySaves { get; }

        public InventorySave(IEnumerable<Item> items, IEnumerable<Currency> currencies)
        {
            ItemSaves = new List<EquipmentStateSave>();
            foreach (var item in items)
            {
                ItemSaves.Add(new EquipmentStateSave(item as Equipment));
            }

            CurrencySaves = new List<CurrencySave>();
            foreach (var currency in currencies)
            {
                CurrencySaves.Add(new CurrencySave(currency));
            }
        }

        public List<Item> GetItems()
        {
            var items = new List<Item>();
            var resourceManager = ResourceManager.Instance;
            foreach (var itemSave in ItemSaves)
            {
                var item = resourceManager.GetEquipmentByID(itemSave.guid)
                    .CreateDeepClone()
                    .SetValuesFromSave(itemSave);
                items.Add(item);
            }

            return items;
        }

        public List<Currency> GetCurrencies()
        {
            var currencies = new List<Currency>();
            var resourceManager = ResourceManager.Instance;
            foreach (var currencySave in CurrencySaves)
            {
                var currency = resourceManager.GetCurrencyByID(currencySave.guid);

                currencies.Add(new Currency(currency, currencySave.amount));
            }

            return currencies;
        }
    }
}