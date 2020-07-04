using System;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public interface IAbilityActivator
    {
        void Activate();
        bool IsActive { get; }
        event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }

    public class AbilityActivatorStatusChangedEventArgs : EventArgs
    {
        public float value;

        public AbilityActivatorStatusChangedEventArgs(float value)
        {
            this.value = value;
        }
    }
}