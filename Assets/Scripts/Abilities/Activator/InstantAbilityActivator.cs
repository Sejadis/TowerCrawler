using System;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities.Activator
{
    public class InstantAbilityActivator : IAbilityActivator
    {
        private readonly Action callback;

        public InstantAbilityActivator(Action callback)
        {
            this.callback = callback;
        }

        public void Activate()
        {
            callback();
        }

        public void Interrupt()
        {
        }

        public bool IsActive => false;
        public event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}