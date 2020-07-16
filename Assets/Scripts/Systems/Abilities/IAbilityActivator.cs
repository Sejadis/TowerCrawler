using System;

namespace SejDev.Systems.Abilities
{
    public interface IAbilityActivator
    {
        bool IsActive { get; }
        void Activate();
        void Interrupt();
        event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}