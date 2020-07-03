using System;

namespace SejDev.Systems.Abilities
{
    public interface IAbility
    {
        void ChangeAbility(Ability ability, AbilitySlot slot);
        event EventHandler OnPreAbilityChanged;
        event EventHandler OnPostAbilityChanged;
        event EventHandler OnPreAbilityActivation;
        event EventHandler OnPostAbilityActivation;
    }
}