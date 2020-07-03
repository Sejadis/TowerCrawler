using SejDev.Systems.Gear;

namespace Editor.Tests.EditorTests.Builder
{
    public class EquipmentHolderBuilder
    {
        IInventory inventory;
        Equipment weaponSlot;
        public EquipmentHolderBuilder(IInventory inventory, Equipment weaponSlot = null)
        {
            this.inventory = inventory;
            this.weaponSlot = weaponSlot;
        }

        // public EquipmentHolderBuilder() : this(5,new List<Item>(),new Dictionary<EquipSlotType, Item>())
        // {

        // }

        public EquipmentHolderBuilder WithItemInSlot(Equipment item){
            switch (item.EquipSlot)
            {
                case EquipSlotType.Weapon:{
                    weaponSlot = item;
                    break;
                }
                default:
                    break;
            }
            return this;
        }

        public EquipmentHolder Build(){
            return new EquipmentHolder(inventory, weaponSlot);
        }

        public static implicit operator EquipmentHolder(EquipmentHolderBuilder equipmentHolderBuilder){
            return equipmentHolderBuilder.Build();
        }
    }
}