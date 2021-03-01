using System;

namespace SejDev.Systems.Core
{
    [Flags]
    public enum Rarity
    {
        Common = 1 << 0,
        Uncommon = 1 << 1,
        Rare = 1 << 2,
        Epic = 1 << 3,
        Legendary = 1 << 4
    }
}