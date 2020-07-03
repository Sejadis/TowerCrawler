using System;

namespace SejDev.Systems.Stats
{
    public class StatChangedEventArgs : EventArgs
    {
        public float OldValue { get; }
        public float NewValue { get; }

        public StatChangedEventArgs(float oldValue, float newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}