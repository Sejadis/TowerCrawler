using System;

namespace SejDev.Systems.Stats
{
    public interface IStats
    {
        event EventHandler OnAnyStatChanged;

        void AddModifier(StatType type, Modifier modifier);
        Stat GetStatByType(StatType type);
        void RemoveModifier(StatType type, Modifier modifier);
    }
}