using System;
using JetBrains.Annotations;

namespace SejDev.Systems.Abilities
{
    public class AbilityChangedEventArgs : EventArgs
    {
        public Ability ability;
        public AbilitySlot slot;

        public AbilityChangedEventArgs(AbilitySlot slot, [NotNull] Ability ability)
        {
            this.slot = slot;
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}