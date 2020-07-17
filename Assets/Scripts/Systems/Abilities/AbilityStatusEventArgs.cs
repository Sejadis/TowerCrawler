using System;
using JetBrains.Annotations;

namespace SejDev.Systems.Abilities
{
    public class AbilityStatusEventArgs : EventArgs
    {
        public readonly Ability ability;
        public float modifiedCastTime;

        public AbilityStatusEventArgs([NotNull] Ability ability)
        {
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}