using System;
using JetBrains.Annotations;
using SejDev.Editor;
using SejDev.Save;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Systems.Abilities
{
    public class AbilityHandler : MonoBehaviour, IAbility
    {
        [CanBeNull] private Ability core1;
        [CanBeNull] private Ability core2;
        [CanBeNull] private Ability core3;
        [CanBeNull] private Ability weaponBase;
        [CanBeNull] private Ability weaponSpecial;

        public event EventHandler OnPreAbilityChanged;
        public event EventHandler<AbilityChangedEventArgs> OnPostAbilityChanged;
        public event EventHandler<AbilityStatusEventArgs> OnPreAbilityActivation;
        public event EventHandler<AbilityStatusEventArgs> OnAbilityInterrupted;
        public event EventHandler<AbilityStatusEventArgs> OnPostAbilityActivation;

        [field: SerializeField, Rename] public Transform AbilityOrigin { get; private set; }
        [field: SerializeField, Rename] public Camera TargetingCamera { get; private set; }

        private Stat castTimeStat;
        private float cooldownRate = 1;
        private Ability activeAbility;
        IWeapon weaponHandler;

        public void ChangeAbility(Ability ability, AbilitySlot slot, WeaponController weaponController = null)
        {
            if (ability == null) return;
            OnPreAbilityChanged?.Invoke(this, null);
            ref var intendedSlot = ref GetSlot(slot);
            if (intendedSlot != null)
            {
                intendedSlot.OnPreAbilityActivation -= RaiseOnPreAbilityActivation;
                intendedSlot.OnPostAbilityActivation -= RaiseOnPostAbilityActivation;
                intendedSlot.OnAbilityInterrupted -= RaiseOnAbilityInterrupted;
            }

            intendedSlot = ability.CreateDeepClone();
            intendedSlot.OnPreAbilityActivation += RaiseOnPreAbilityActivation;
            intendedSlot.OnPostAbilityActivation += RaiseOnPostAbilityActivation;
            intendedSlot.OnAbilityInterrupted += RaiseOnAbilityInterrupted;
            if (ability is WeaponAbility &&
                (slot == AbilitySlot.WeaponBase || slot == AbilitySlot.WeaponSpecial))
            {
                (intendedSlot as WeaponAbility).Bind(this, weaponController, castTimeStat);
            }
            else
            {
                intendedSlot.Bind(this, castTimeStat);
            }

            OnPostAbilityChanged?.Invoke(this, new AbilityChangedEventArgs(slot, intendedSlot));
        }

        public Ability GetAbilityBySlot(AbilitySlot slot)
        {
            switch (slot)
            {
                case AbilitySlot.WeaponBase:
                    return weaponBase;
                case AbilitySlot.WeaponSpecial:
                    return weaponSpecial;
                case AbilitySlot.Core1:
                    return core1;
                case AbilitySlot.Core2:
                    return core2;
                case AbilitySlot.Core3:
                    return core3;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
            }
        }

        private void Start()
        {
            weaponHandler = GetComponent<IWeapon>();
            var handler = GetComponent<IStats>();
            if (handler != null)
            {
                var cooldownRateStat = handler.GetStatByType(StatType.CooldownRate);
                if (cooldownRateStat != null)
                {
                    cooldownRate = cooldownRateStat.Value;
                    cooldownRateStat.OnStatChanged += (s, args) => cooldownRate = args.NewValue;
                }

                castTimeStat = handler.GetStatByType(StatType.CastTime);
            }

            var movementHandler = GetComponent<IEntityController>();
            if (movementHandler != null)
            {
                movementHandler.OnMoveStateChanged += OnOnMoveStateChanged;
            }

            LoadEquippedAbilities();
        }


        private void LoadEquippedAbilities()
        {
            var save = SaveManager.GetSave<EquippedAbilitySave>();
            if (save == null) return;
            if (!string.IsNullOrEmpty(save.core1ID))
            {
                var core1 = ResourceManager.Instance.GetAbilityByID(save.core1ID);
                if (core1 != null)
                {
                    ChangeAbility(core1, AbilitySlot.Core1);
                }
            }

            if (!string.IsNullOrEmpty(save.core2ID))
            {
                var core2 = ResourceManager.Instance.GetAbilityByID(save.core2ID);
                if (core2 != null)
                {
                    ChangeAbility(core2, AbilitySlot.Core2);
                }
            }

            if (!string.IsNullOrEmpty(save.core3ID))
            {
                var core3 = ResourceManager.Instance.GetAbilityByID(save.core3ID);
                if (core3 != null)
                {
                    ChangeAbility(core3, AbilitySlot.Core3);
                }
            }
        }

        private void OnOnMoveStateChanged(object sender, bool e)
        {
            activeAbility?.Interrupt();
        }

        private void OnEnable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnCore1 += ActivateCore1;
                InputManager.Instance.OnCore2 += ActivateCore2;
                InputManager.Instance.OnCore3 += ActivateCore3;
                InputManager.Instance.OnWeaponBase += ActivateWeaponBase;
                InputManager.Instance.OnWeaponSpecial += ActivateWeaponSpecial;
            }
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnCore1 -= ActivateCore1;
                InputManager.Instance.OnCore2 -= ActivateCore2;
                InputManager.Instance.OnCore3 -= ActivateCore3;
                InputManager.Instance.OnWeaponBase -= ActivateWeaponBase;
                InputManager.Instance.OnWeaponSpecial -= ActivateWeaponSpecial;
            }
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

        private void RaiseOnPreAbilityActivation(object sender, AbilityStatusEventArgs e)
        {
            //TODO consider original sender
            activeAbility = e.ability;
            OnPreAbilityActivation?.Invoke(this, e);
        }

        private void RaiseOnAbilityInterrupted(object sender, EventArgs e)
        {
            OnAbilityInterrupted?.Invoke(this, new AbilityStatusEventArgs(activeAbility));
            activeAbility = null;
        }

        private void RaiseOnPostAbilityActivation(object sender, AbilityStatusEventArgs e)
        {
            //TODO consider original sender
            activeAbility = null;
            OnPostAbilityActivation?.Invoke(this, e);
        }

        private ref Ability GetSlot(AbilitySlot slot)
        {
            switch (slot)
            {
                case AbilitySlot.WeaponBase:
                    return ref weaponBase;
                case AbilitySlot.WeaponSpecial:
                    return ref weaponSpecial;
                case AbilitySlot.Core1:
                    return ref core1;
                case AbilitySlot.Core2:
                    return ref core2;
                case AbilitySlot.Core3:
                    return ref core3;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
            }
        }

        public void ActivateCore1(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            // Debug.Log($"got input with phase {context.phase}");
            if (activeAbility != null || (core1?.CanActivate != null && !core1.CanActivate)) return;

            core1?.Activate();
        }

        public void ActivateCore2(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            if (activeAbility != null || (core2?.CanActivate != null && !core2.CanActivate)) return;

            core2?.Activate();
        }

        public void ActivateCore3(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            if (activeAbility != null || (core3?.CanActivate != null && !core3.CanActivate)) return;

            core3?.Activate();
        }

        public void ActivateWeaponBase(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            if (activeAbility != null || (weaponBase?.CanActivate != null && !weaponBase.CanActivate)) return;

            (weaponBase as WeaponAbility)?.Activate();
        }

        public void ActivateWeaponSpecial(InputAction.CallbackContext context)
        {
            // Debug.Log(context.ToString());
            if (activeAbility != null || (weaponSpecial?.CanActivate != null && !weaponSpecial.CanActivate)) return;

            (weaponSpecial as WeaponAbility)?.Activate();
        }

        public void ReloadAbilities()
        {
            LoadEquippedAbilities();
        }
    }
}