namespace SejDev.Systems.Gear
{
    public abstract class Equipment : Item
    {
        public abstract EquipSlotType EquipSlot { get; protected set; }
    }
}