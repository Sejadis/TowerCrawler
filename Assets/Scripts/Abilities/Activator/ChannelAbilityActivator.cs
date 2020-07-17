using System;

namespace SejDev.Systems.Abilities
{
    public class ChannelAbilityActivator : IAbilityActivator
    {
        public void Activate(float activationModifier)
        {
            throw new NotImplementedException();
        }

        public void Interrupt()
        {
            throw new NotImplementedException();
        }

        public bool IsActive { get; }
        public event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}