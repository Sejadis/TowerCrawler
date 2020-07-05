using System;
using JetBrains.Annotations;

namespace SejDev.Systems.Abilities
{
    public class AbilityActivationEventArgs : EventArgs
    {
        public Ability ability;

        public AbilityActivationEventArgs([NotNull] Ability ability)
        {
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}