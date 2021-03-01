using System.Collections.ObjectModel;

namespace SejDev.Systems.Equipment
{
    public interface IInventory
    {
        int AvailableSpace { get; }
        ReadOnlyCollection<Item> Items { get; }

        void AddItem(Equipment item);
        void AddCurrency(CurrencyData currencyData, int amount);
        void RemoveCurrency(CurrencyData currencyData, int amount);
        void RemoveItem(Equipment item);
        bool ContainsItem(Equipment item);
        bool ContainsCurrency(CurrencyData currencyData, int amount);
    }
}