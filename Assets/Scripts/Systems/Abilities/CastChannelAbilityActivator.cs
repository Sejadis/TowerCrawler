using System;

namespace SejDev.Systems.Abilities
{
    public class CastChannelAbilityActivator : IAbilityActivator
    {
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public bool IsActive { get; }
        public event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}