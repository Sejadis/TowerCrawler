namespace SejDev.Systems.Gear
{
    public class Equipment : Item
    {
        public Equipment(EquipSlotType equipSlot = EquipSlotType.Weapon)
        {
            EquipSlot = equipSlot;
        }

        public EquipSlotType EquipSlot { get; }
    }
}