using System;
using NSubstitute;
using NUnit.Framework;
using SejDev.Systems.Gear;

namespace Editor.Tests.EditorTests
{
    public class EquipmentHolderTests
    {
        public class EquipItem
        {
            IInventory inventory;
            Equipment equipment;
            EquipmentHolder equipmentHolder;

            [SetUp]
            public void SetUp()
            {
                inventory = Substitute.For<IInventory>();
                //equipment = A.Equipment().WithSlotType(EquipSlotType.Weapon);
                // equipmentHolder = A.EquipmentHolder(inventory);
            }

            [Test]
            public void Item_Is_Equippped_To_Correct_Slot()
            {
                inventory.ContainsItem(equipment).Returns(true);

                equipmentHolder.EquipItem(equipment);

                Assert.AreEqual(equipment, equipmentHolder.GetItemForSlot(equipment.EquipSlot));
            }

            [Test]
            public void Item_Is_Removed_From_Inventory()
            {
                inventory.ContainsItem(equipment).Returns(true);

                equipmentHolder.EquipItem(equipment);

                inventory.Received().RemoveItem(equipment);
            }

            [Test]
            public void Item_Will_Be_Equipped_When_Slot_Currently_Full()
            {
                //Equipment newItem = A.Equipment().WithSlotType(EquipSlotType.Weapon);
                // EquipmentHolder equipmentHolder = A.EquipmentHolder(inventory).WithItemInSlot(equipment);
                //inventory.ContainsItem(newItem).Returns(true);

                //equipmentHolder.EquipItem(newItem);

                //Assert.AreEqual(newItem, equipmentHolder.GetItemForSlot(EquipSlotType.Weapon));
            }

            [Test]
            public void Currently_Equipped_Item_Will_Be_Added_To_Inventory_When_New_Item_Is_Equipped()
            {
                // Equipment newItem = A.Equipment().WithSlotType(EquipSlotType.Weapon);
                // inventory.ContainsItem(newItem).Returns(true);
                // EquipmentHolder equipmentHolder = A.EquipmentHolder(inventory).WithItemInSlot(equipment);
                //
                // equipmentHolder.EquipItem(newItem);
                //
                // inventory.Received().AddItem(equipment);
            }

            [Test]
            public void Throws_When_Item_Not_In_Inventory()
            {
                inventory.ContainsItem(equipment).Returns(false);

                var exception = Assert.Throws<InvalidOperationException>(() => equipmentHolder.EquipItem(equipment));
                Assert.AreEqual("Item not in Inventory", exception.Message);
            }
        }
    }
}