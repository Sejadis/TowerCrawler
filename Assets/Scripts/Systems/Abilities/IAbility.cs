using System;
using UnityEngine;

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
}