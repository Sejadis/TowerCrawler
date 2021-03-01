using NUnit.Framework;
using SejDev.Systems.Equipment;

namespace Editor.Tests.EditorTests
{
    public class InventoryTest
    {
        public class AddItem
        {
            Item item;

            [SetUp]
            public void SetUp()
            {
                // item = A.Equipment();
            }

            // [Test]
            // public void Item_Is_In_Inventory()
            // {
            //     Inventory inventory = A.Inventory();
            //
            //     inventory.AddItem(item);
            //
            //     Assert.IsTrue(inventory.Items.Contains(item));
            // }
            //
            // [Test]
            // public void Available_Inventory_Space_Reduced()
            // {
            //     Inventory inventory = A.Inventory().WithMaxSpace(6);
            //
            //     inventory.AddItem(item);
            //
            //     //Assert.AreEqual(5, inventory.AvailableSpace);
            // }

            // [Test]
            // public void Throw_When_Full()
            // {
            //     Inventory inventory = A.Inventory().WithMaxSpace(0);
            //
            //     var exception = Assert.Throws<InvalidOperationException>(() => inventory.AddItem(item));
            //     Assert.AreEqual("Inventory full", exception.Message);
            // }
        }

        public class RemoveItem
        {
            Item item;

            [SetUp]
            public void SetUp()
            {
                //item = A.Equipment();
            }

            // [Test]
            // public void Throws_When_Item_Not_In_Inventory()
            // {
            //     Inventory inventory = A.Inventory();
            //
            //     var exception = Assert.Throws<InvalidOperationException>(() => inventory.RemoveItem(item));
            //     Assert.AreEqual("Item not in Inventory", exception.Message);
            // }

            // [Test]
            // public void Item_Is_Removed_From_Inventory()
            // {
            //     Inventory inventory = A.Inventory().WithItems(new List<Item>() {item});
            //
            //     inventory.RemoveItem(item);
            //
            //     Assert.IsFalse(inventory.Items.Contains(item));
            // }
        }

        public class ContainsItem
        {
            Item item;

            [SetUp]
            public void SetUp()
            {
                //item = A.Equipment();
            }

            // [Test]
            // public void Returns_True_When_Item_In_Inventory()
            // {
            //     Inventory inventory = A.Inventory().WithItems(new List<Item>() {item});
            //
            //     Assert.IsTrue(inventory.ContainsItem(item));
            // }
            //
            // [Test]
            // public void Returns_False_When_Item__Not_In_Inventory()
            // {
            //     Inventory inventory = A.Inventory();
            //
            //     Assert.IsFalse(inventory.ContainsItem(item));
            // }
        }
    }
}