using System;

namespace SejDev.Systems.Abilities
{
    public interface IAbilityActivator
    {
        bool IsActive { get; }
        void Activate(float activationModifier);
        void Interrupt();
        event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}