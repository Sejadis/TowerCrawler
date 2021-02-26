using System;

namespace SejDev.Systems.Abilities
{
    public class AbilityChangedEventArgs : EventArgs
    {
        public Ability ability;
        public AbilitySlot slot;

        public AbilityChangedEventArgs(AbilitySlot slot, Ability ability)
        {
            this.slot = slot;
            this.ability = ability;
        }
    }
}