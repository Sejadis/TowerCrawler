using System;

namespace SejDev.Systems.Equipment
{
    //TODO prevent multiple values for things like item SO
    [Flags]
    public enum EquipSlotType
    {
        Weapon = 1 << 0,
        Head = 1 << 1,
        Shoulder = 1 << 2,
        Chest = 1 << 3,
        Arms = 1 << 4,
        Hands = 1 << 5,
        Legs = 1 << 6,
        Feet = 1 << 7,
    }
}