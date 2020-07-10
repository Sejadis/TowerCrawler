﻿using System;
using JetBrains.Annotations;
using SejDev.Editor;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace SejDev.Systems.Abilities
{
    public class AbilityManager : MonoBehaviour, IAbility
    {
        [CanBeNull] private Ability core1;
        [CanBeNull] private Ability core2;
        [CanBeNull] private Ability core3;
        [CanBeNull] private Ability weaponBase;
        [CanBeNull] private Ability weaponSpecial;

        public event EventHandler OnPreAbilityChanged;

        public event EventHandler<AbilityChangedEventArgs> OnPostAbilityChanged;

        public event EventHandler<AbilityActivationEventArgs> OnPreAbilityActivation;

        public event EventHandler<AbilityActivationEventArgs> OnPostAbilityActivation;
        [field: SerializeField, Rename] public Transform AbilityOrigin { get; private set; }
        [field: SerializeField, Rename] public Camera TargetingCamera { get; private set; }

        private Stat castTimeStat;
        private float cooldownRate = 1;

        public void ChangeAbility(Ability ability, AbilitySlot slot)
        {
            OnPreAbilityChanged?.Invoke(this, null);
            ref var intendedSlot = ref GetSlot(slot);
            if (intendedSlot != null)
            {
                intendedSlot.OnPreAbilityActivation -= RaiseOnPreAbilityActivation;
                intendedSlot.OnPostAbilityActivation -= RaiseOnPostAbilityActivation;
            }

            intendedSlot = Instantiate(ability);
            intendedSlot.OnPreAbilityActivation += RaiseOnPreAbilityActivation;
            intendedSlot.OnPostAbilityActivation += RaiseOnPostAbilityActivation;
            intendedSlot.Bind(this);
            OnPostAbilityChanged?.Invoke(this, new AbilityChangedEventArgs(slot, intendedSlot));
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

        private void Start()
        {
            var handler = GetComponent<IStats>();
            if (handler != null)
            {
                var cooldownRateStat = handler.GetStatByType(StatType.CooldownRate);
                if (cooldownRateStat != null)
                {
                    cooldownRate = cooldownRateStat.Value;
                    cooldownRateStat.OnStatChanged += (s, args) => cooldownRate = args.NewValue;
                }

                // castTimeStat = handler?.GetStatByType(StatType.CastTime);
            }
        }


        private void OnEnable()
        {
            InputManager.Instance.OnCore1 += ActivateCore1;
            InputManager.Instance.OnCore2 += ActivateCore2;
            InputManager.Instance.OnCore3 += ActivateCore3;
            InputManager.Instance.OnWeaponBase += ActivateWeaponBase;
            InputManager.Instance.OnWeaponSpecial += ActivateWeaponSpecial;

            // // InputManager.Instance.PlayerInput.Controls.Core1.started += ActivateCore1;
            // InputManager.Instance.PlayerInput.Controls.Core1.performed += ActivateCore1;
            // // InputManager.Instance.PlayerInput.Controls.Core1.canceled += ActivateCore1;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.Core2.started += ActivateCore2;
            // InputManager.Instance.PlayerInput.Controls.Core2.performed += ActivateCore2;
            // // InputManager.Instance.PlayerInput.Controls.Core2.canceled += ActivateCore2;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.Core3.started += ActivateCore3;
            // InputManager.Instance.PlayerInput.Controls.Core3.performed += ActivateCore3;
            // // InputManager.Instance.PlayerInput.Controls.Core3.canceled += ActivateCore3;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.WeaponBase.started += ActivateWeaponBase;
            // InputManager.Instance.PlayerInput.Controls.WeaponBase.performed += ActivateWeaponBase;
            // // InputManager.Instance.PlayerInput.Controls.WeaponBase.canceled += ActivateWeaponBase;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.WeaponSpecial.started += ActivateWeaponSpecial;
            // InputManager.Instance.PlayerInput.Controls.WeaponSpecial.performed += ActivateWeaponSpecial;
            // // InputManager.Instance.PlayerInput.Controls.WeaponSpecial.canceled += ActivateWeaponSpecial;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnCore1 -= ActivateCore1;
            InputManager.Instance.OnCore2 -= ActivateCore2;
            InputManager.Instance.OnCore3 -= ActivateCore3;
            InputManager.Instance.OnWeaponBase -= ActivateWeaponBase;
            InputManager.Instance.OnWeaponSpecial -= ActivateWeaponSpecial;

            // // InputManager.Instance.PlayerInput.Controls.Core1.started -= ActivateCore1;
            // InputManager.Instance.PlayerInput.Controls.Core1.performed -= ActivateCore1;
            // // InputManager.Instance.PlayerInput.Controls.Core1.canceled -= ActivateCore1;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.Core2.started -= ActivateCore2;
            // InputManager.Instance.PlayerInput.Controls.Core2.performed -= ActivateCore2;
            // // InputManager.Instance.PlayerInput.Controls.Core2.canceled -= ActivateCore2;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.Core3.started -= ActivateCore3;
            // InputManager.Instance.PlayerInput.Controls.Core3.performed -= ActivateCore3;
            // // InputManager.Instance.PlayerInput.Controls.Core3.canceled -= ActivateCore3;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.WeaponBase.started -= ActivateWeaponBase;
            // InputManager.Instance.PlayerInput.Controls.WeaponBase.performed -= ActivateWeaponBase;
            // // InputManager.Instance.PlayerInput.Controls.WeaponBase.canceled -= ActivateWeaponBase;
            //
            //
            // // InputManager.Instance.PlayerInput.Controls.WeaponSpecial.started -= ActivateWeaponSpecial;
            // InputManager.Instance.PlayerInput.Controls.WeaponSpecial.performed -= ActivateWeaponSpecial;
            // // InputManager.Instance.PlayerInput.Controls.WeaponSpecial.canceled -= ActivateWeaponSpecial;
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            delta *= cooldownRate;
            weaponBase?.UpdateCooldown(delta);
            weaponSpecial?.UpdateCooldown(delta);
            core1?.UpdateCooldown(delta);
            core2?.UpdateCooldown(delta);
            core3?.UpdateCooldown(delta);
        }

        private void RaiseOnPostAbilityActivation(object sender, AbilityActivationEventArgs e)
        {
            //TODO consider original sender
            OnPostAbilityActivation?.Invoke(this, e);
        }

        private void RaiseOnPreAbilityActivation(object sender, AbilityActivationEventArgs e)
        {
            //TODO consider original sender
            OnPreAbilityActivation?.Invoke(this, e);
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

            if (int.TryParse(Console.ReadLine(), out var input))
            {
                if (input == 1)
                {
                    //leave
                }
            }
            else
            {
                //tell the user he is to stupid to put in a number
            }
        }

        public void ActivateCore1(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            // Debug.Log($"got input with phase {context.phase}");
            if (core1?.RemainingCooldown > 0) return;

            core1?.Activate();
        }

        public void ActivateCore2(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            if (core2?.CanActivate != null && !core2.CanActivate) return;

            core2?.Activate();
        }

        public void ActivateCore3(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            if (core3?.CanActivate != null && !core3.CanActivate) return;

            core3?.Activate();
        }

        public void ActivateWeaponBase(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (weaponBase?.RemainingCooldown > 0) return;

            weaponBase?.Activate();
        }

        public void ActivateWeaponSpecial(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (weaponSpecial?.RemainingCooldown > 0) return;

            weaponSpecial?.Activate();
        }
    }
}