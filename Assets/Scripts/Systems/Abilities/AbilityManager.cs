using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Systems.Abilities
{
    public class AbilityManager : MonoBehaviour, IAbility
    {
        [CanBeNull] private Ability weaponBase;
        [CanBeNull] private Ability weaponSpecial;
        [CanBeNull] private Ability core1;
        [CanBeNull] private Ability core2;
        [CanBeNull] private Ability core3;

        public event EventHandler OnPreAbilityChanged;

        public event EventHandler OnPostAbilityChanged;

        public event EventHandler OnPreAbilityActivation;

        public event EventHandler OnPostAbilityActivation;

        private void Update()
        {
            weaponBase?.UpdateCooldown(Time.deltaTime);
            weaponSpecial?.UpdateCooldown(Time.deltaTime);
            core1?.UpdateCooldown(Time.deltaTime);
            core2?.UpdateCooldown(Time.deltaTime);
            core3?.UpdateCooldown(Time.deltaTime);
        }

        public void ChangeAbility(Ability ability, AbilitySlot slot)
        {
            OnPreAbilityChanged?.Invoke(this, null);
            ref Ability intendedSlot = ref GetSlot(slot);
            intendedSlot = ability;
            ability.Bind(this);
            OnPostAbilityChanged?.Invoke(this, null);
        }

        public void ActivateCore1(InputAction.CallbackContext context)
        {
            if (core1?.RemainingCooldown > 0)
            {
                return;
            }
            core1?.Activate();
        }
        
        public void ActivateCore2(InputAction.CallbackContext context)
        {
            if (core2?.RemainingCooldown > 0)
            {
                return;
            }
            core2?.Activate();
        }
        public void ActivateCore3(InputAction.CallbackContext context)
        {
            if (core3?.RemainingCooldown > 0)
            {
                return;
            }
            core3?.Activate();
        }

        public void ActivateWeaponBase(InputAction.CallbackContext context)
        {
            if (weaponBase?.RemainingCooldown > 0)
            {
                return;
            }
            weaponBase?.Activate();
        }
        
        public void ActivateWeaponSpecial(InputAction.CallbackContext context)
        {
            if (weaponSpecial?.RemainingCooldown > 0)
            {
                return;
            }
            weaponSpecial?.Activate();
        }
        
        private ref Ability GetSlot(AbilitySlot slot)
        {
            switch (slot)
            {
                case AbilitySlot.WeaponBase:
                    return ref weaponBase;
                    break;
                case AbilitySlot.WeaponSpecial:
                    return ref weaponSpecial;
                    break;
                case AbilitySlot.Core1:
                    return ref core1;
                    break;
                case AbilitySlot.Core2:
                    return ref core2;
                    break;
                case AbilitySlot.Core3:
                    return ref core3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
            }
        }
    }
}