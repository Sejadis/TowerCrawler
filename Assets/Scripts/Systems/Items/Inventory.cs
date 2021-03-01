using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SejDev.Systems.Equipment
{
    public class Inventory : IInventory
    {
        private readonly List<Item> items;
        public int MaxSpace { get; }
        public Action OnInventoryChanged;
        private List<Currency> currencies = new List<Currency>();

        public Inventory(int maxSpace)
        {
            MaxSpace = maxSpace;
        }

        public Inventory(int maxSpace, List<Item> items = null, List<Currency> currencies = null) : this(maxSpace)
        {
            this.items = items ?? new List<Item>();
            this.currencies = currencies ?? new List<Currency>();
        }

        public Inventory() : this(10)
        {
        }

        public int AvailableSpace => MaxSpace - items.Count;
        public ReadOnlyCollection<Item> Items => items.AsReadOnly();
        public ReadOnlyCollection<Currency> Currencies => currencies.AsReadOnly();

        public void AddItem(Equipment item)
        {
            if (AvailableSpace <= 0) throw new InvalidOperationException("Inventory full");
            items.Add(item);
            OnInventoryChanged.Invoke();
        }

        public void AddCurrency(CurrencyData currencyData, int amount)
        {
            var currency = currencies.Find(curr => curr.CurrencyData.Equals(currencyData));
            if (currency == null)
            {
                currency = new Currency(currencyData, amount);
                currencies.Add(currency);
            }
            else
            {
                currency.Amount += amount;
            }

            OnInventoryChanged.Invoke();
        }

        public void RemoveCurrency(CurrencyData currencyData, int amount)
        {
            var currency = currencies.Find(curr => curr.CurrencyData.Equals(currencyData));
            if (currency == null)
            {
                throw new Exception($"Trying to remove currency {currencyData.name} when it doesnt exist in inventory");
            }

            if (currency.Amount < amount)
            {
                throw new InvalidOperationException(
                    $"Trying to remove more currency from inventory than it contains ({currencyData.name})");
            }

            currency.Amount -= amount;
            OnInventoryChanged.Invoke();
        }

        public void RemoveItem(Equipment item)
        {
            if (!items.Contains(item)) throw new InvalidOperationException("Item not in Inventory");
            items.Remove(item);
            OnInventoryChanged.Invoke();
        }

        public bool ContainsItem(Equipment item)
        {
            return items.Contains(item);
        }

        public bool ContainsCurrency(CurrencyData currencyData, int amount)
        {
            var currency = currencies.Find(curr => curr.CurrencyData.Equals(currencyData));
            return currency != null && currency.Amount >= amount;
        }
    }
}