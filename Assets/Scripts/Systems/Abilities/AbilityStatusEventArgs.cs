using System;
using JetBrains.Annotations;

namespace SejDev.Systems.Abilities
{
    public class AbilityStatusEventArgs : EventArgs
    {
        public Ability ability;

        public AbilityStatusEventArgs([NotNull] Ability ability)
        {
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}