using System;

namespace SejDev.Systems.Stats
{
    [Flags]
    public enum StatType
    {
        MovementSpeed = 1 << 0,
        CastTime = 1 << 1,
        CooldownRate = 1 << 2,
        MidAirJump = 1 << 3,
        Damage = 1 << 4,
        Health = 1 << 5,
    }
}