using System;

namespace SejDev.Systems.Core
{
    [Flags]
    public enum DamageFlags
    {
        None = 0,
        IgnoresMitigation = 1 << 0,
    }
}