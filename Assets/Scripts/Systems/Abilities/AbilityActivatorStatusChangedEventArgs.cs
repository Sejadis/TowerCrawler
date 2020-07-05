using System;

namespace SejDev.Systems.Abilities
{
    public class AbilityActivatorStatusChangedEventArgs : EventArgs
    {
        public float value;

        public AbilityActivatorStatusChangedEventArgs(float value)
        {
            this.value = value;
        }
    }
}