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

        public event EventHandler<AbilityChangedEventArgs> OnPostAbilityChanged;

        public event EventHandler<AbilityActivationEventArgs> OnPreAbilityActivation;

        public event EventHandler<AbilityActivationEventArgs> OnPostAbilityActivation;

        // private void Start()
        // {
        //     PlayerInput input = GetComponent<PlayerInput>();
        // }

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
            if (intendedSlot != null)
            {
                intendedSlot.OnPreAbilityActivation -= RaiseOnPreAbilityActivation;
                intendedSlot.OnPostAbilityActivation -= RaiseOnPostAbilityActivation;
            }
            intendedSlot = ability;
            intendedSlot.OnPreAbilityActivation += RaiseOnPreAbilityActivation;
            intendedSlot.OnPostAbilityActivation += RaiseOnPostAbilityActivation;
            ability.Bind(this);
            OnPostAbilityChanged?.Invoke(this, new AbilityChangedEventArgs(slot,ability));
        }

        private void RaiseOnPostAbilityActivation(object sender, AbilityActivationEventArgs e)
        {
            //TODO consider original sender
            OnPostAbilityActivation?.Invoke(this,e);
        }

        private void RaiseOnPreAbilityActivation(object sender, AbilityActivationEventArgs e)
        {
            //TODO consider original sender
            OnPreAbilityActivation?.Invoke(this,e);
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

        public Ability GetAbilityBySlot(AbilitySlot slot)
        {
            switch (slot)
            {
                case AbilitySlot.WeaponBase:
                    return weaponBase;
                    break;
                case AbilitySlot.WeaponSpecial:
                    return weaponSpecial;
                    break;
                case AbilitySlot.Core1:
                    return core1;
                    break;
                case AbilitySlot.Core2:
                    return core2;
                    break;
                case AbilitySlot.Core3:
                    return core3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
            }
        }

        public void ActivateCore1(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            // Debug.Log($"got input with phase {context.phase}");
            if (core1?.RemainingCooldown > 0)
            {
                return;
            }

            core1?.Activate();
        }

        public void ActivateCore2(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (core2?.RemainingCooldown > 0)
            {
                return;
            }

            core2?.Activate();
        }

        public void ActivateCore3(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (core3?.RemainingCooldown > 0)
            {
                return;
            }

            core3?.Activate();
        }

        public void ActivateWeaponBase(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (weaponBase?.RemainingCooldown > 0)
            {
                return;
            }

            weaponBase?.Activate();
        }

        public void ActivateWeaponSpecial(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (weaponSpecial?.RemainingCooldown > 0)
            {
                return;
            }

            weaponSpecial?.Activate();
        }
    }
}