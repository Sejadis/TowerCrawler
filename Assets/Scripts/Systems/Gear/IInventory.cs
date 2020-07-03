using System.Collections.ObjectModel;

namespace SejDev.Systems.Gear
{
    public interface IInventory
    {
        int AvailableSpace { get; }
        ReadOnlyCollection<Item> Items { get; }

        void AddItem(Item item);
        void RemoveItem(Item item);
        bool ContainsItem(Item item);
    }
}