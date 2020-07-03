namespace SejDev.Systems.Gear
{
    public class Equipment : Item
    {
        public EquipSlotType EquipSlot { get; private set; }
        public Equipment(EquipSlotType equipSlot = EquipSlotType.Weapon)
        {
            EquipSlot = equipSlot;
        }
    }
}