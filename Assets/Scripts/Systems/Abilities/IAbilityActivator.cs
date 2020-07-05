using System;

namespace SejDev.Systems.Abilities
{
    public interface IAbilityActivator
    {
        bool IsActive { get; }
        void Activate();
        event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}