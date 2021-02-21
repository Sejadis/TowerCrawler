using System;
using SejDev.Systems.Equipment;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public interface IAbility
    {
        Transform AbilityOrigin { get; }
        Camera TargetingCamera { get; }
        void ChangeAbility(Ability ability, AbilitySlot slot, WeaponController weaponController = null);
        Ability GetAbilityBySlot(AbilitySlot slot);
        event EventHandler OnPreAbilityChanged;
        event EventHandler<AbilityChangedEventArgs> OnPostAbilityChanged;
        event EventHandler<AbilityStatusEventArgs> OnPreAbilityActivation;
        event EventHandler<AbilityStatusEventArgs> OnAbilityInterrupted;
        event EventHandler<AbilityStatusEventArgs> OnPostAbilityActivation;
    }
}