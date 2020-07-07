using System;
using SejDev.Abilities.Activator;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        //TODO replace Header attributes by custom inspector
        private AbilityActivationEventArgs abilityActivationEventArgs;
        private IAbility abilityManager;

        [field: Rename]
        [field: SerializeField]
        public string Name { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public Sprite Icon { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public AbilityType Type { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        [field: Header("Cooldown")]
        public float Cooldown { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        [field: Header("Charge")]
        public int Charges { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public float ChargeCooldown { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        [field: Header("Energy")]
        public float Energy { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public float EnergyDrainRate { get; protected set; }

        [field: Rename]
        [field: SerializeField]

        public float EnergyFillRate { get; protected set; }
        //TODO energy drain, max energy, energy rate
        //TODO charge, charge cd

        [field: Rename]
        [field: SerializeField]
        public AbilityActivationType AbilityActivationType { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        [field: Header("Cast")]
        public float CastTime { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        [field: Header("Channel")]
        public float ChannelTime { get; protected set; }

        public bool CanActivate => !AbilityActivator.IsActive && (RemainingCooldown <= 0 || CurrentCharges > 0);
        public float RemainingCooldown { get; protected set; }
        public int CurrentCharges { get; protected set; }

        public IAbilityActivator AbilityActivator { get; protected set; }

        // public IAbilityTargeter AbilityTargeter { get; protected set; }
        public event EventHandler<AbilityActivationEventArgs> OnPreAbilityActivation;
        public event EventHandler<AbilityActivationEventArgs> OnPostAbilityActivation;

        public event EventHandler<OldNewEventArgs<float>> OnCooldownChanged;
        public event EventHandler<OldNewEventArgs<int>> OnChargesChanged;

        public virtual void Bind(IAbility abilityHandler)
        {
            this.abilityManager = abilityHandler;
            abilityActivationEventArgs = new AbilityActivationEventArgs(this);
            switch (AbilityActivationType)
            {
                case AbilityActivationType.Instant:
                    AbilityActivator = new InstantAbilityActivator(PerformAbility);
                    break;
                case AbilityActivationType.Cast:
                    AbilityActivator =
                        new CastAbilityActivator(PerformAbility, CastTime, abilityHandler as MonoBehaviour);
                    break;
                case AbilityActivationType.Channel:
                    AbilityActivator = new ChannelAbilityActivator();
                    break;
                case AbilityActivationType.CastChannel:
                    AbilityActivator = new CastChannelAbilityActivator();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (Type)
            {
                case AbilityType.Cooldown:
                    break;
                case AbilityType.Energy:
                    break;
                case AbilityType.Charge:
                    CurrentCharges = Charges;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Activate()
        {
            // Debug.Log($"call to activate (will be {CanActivate})");
            if (!CanActivate) return;
            OnPreAbilityActivation?.Invoke(this, abilityActivationEventArgs);
            AbilityActivator.Activate();
        }

        public void UpdateCooldown(float deltaTime)
        {
            if (RemainingCooldown == 0) return;

            var old = RemainingCooldown;
            RemainingCooldown -= deltaTime;
            if (RemainingCooldown <= 0)
            {
                RemainingCooldown = 0;
                if (Type == AbilityType.Charge && CurrentCharges < Charges)
                {
                    CurrentCharges++;
                    OnChargesChanged?.Invoke(this, new OldNewEventArgs<int>(CurrentCharges - 1, CurrentCharges));
                    if (CurrentCharges < Charges)
                    {
                        RemainingCooldown += ChargeCooldown;
                    }
                }
            }

            OnCooldownChanged?.Invoke(this, new OldNewEventArgs<float>(old, RemainingCooldown));
        }

        protected virtual void PerformAbility()
        {
            OnPostAbilityActivation?.Invoke(this, abilityActivationEventArgs);
            switch (Type)
            {
                case AbilityType.Cooldown:
                    RemainingCooldown = Cooldown;
                    break;
                case AbilityType.Energy:
                    break;
                case AbilityType.Charge:
                    if (CurrentCharges == Charges)
                    {
                        RemainingCooldown = ChargeCooldown;
                    }

                    CurrentCharges--;
                    OnChargesChanged?.Invoke(this, new OldNewEventArgs<int>(CurrentCharges + 1, CurrentCharges));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public interface IAbilityTargeter<T>
    {
        T GetTarget();
    }
}