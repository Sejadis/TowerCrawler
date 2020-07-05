using System;

namespace SejDev.Systems.Stats
{
    public class StatChangedEventArgs : EventArgs
    {
        public StatChangedEventArgs(float oldValue, float newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public float OldValue { get; }
        public float NewValue { get; }
    }
}