using System;
using JetBrains.Annotations;

namespace SejDev.Systems.Abilities
{
    public interface IAbility
    {
        void ChangeAbility(Ability ability, AbilitySlot slot);
        Ability GetAbilityBySlot(AbilitySlot slot);
        event EventHandler OnPreAbilityChanged;
        event EventHandler<AbilityChangedEventArgs> OnPostAbilityChanged;
        event EventHandler<AbilityActivationEventArgs> OnPreAbilityActivation;
        event EventHandler<AbilityActivationEventArgs> OnPostAbilityActivation;
    }

    public class AbilityActivationEventArgs : EventArgs
    {
        public Ability ability;

        public AbilityActivationEventArgs([NotNull] Ability ability)
        {
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }

    public class AbilityChangedEventArgs: EventArgs
    {
        public AbilitySlot slot;
        public Ability ability;

        public AbilityChangedEventArgs(AbilitySlot slot, [NotNull] Ability ability)
        {
            this.slot = slot;
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}