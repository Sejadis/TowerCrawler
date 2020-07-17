using System;
using JetBrains.Annotations;
using NSubstitute;
using SejDev.Abilities.Activator;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    [Serializable]
    public abstract class Ability : ScriptableObject, IEquatable<Ability>
    {
        public bool Equals(Ability other)
        {
            return base.Equals(other) && guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ability) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ guid.GetHashCode();
            }
        }

        private Guid guid = Guid.NewGuid();

        //TODO replace Header attributes by custom inspector
        private AbilityStatusEventArgs abilityStatusEventArgs;
        protected IAbility abilityManager;

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

        public bool CanActivate => (AbilityActivator == null || !AbilityActivator.IsActive) &&
                                   (RemainingCooldown <= 0 || CurrentCharges > 0);

        public float RemainingCooldown { get; protected set; }
        public int CurrentCharges { get; protected set; }

        public IAbilityActivator AbilityActivator { get; protected set; }

        private IAbilityTargeter AbilityTargeter;
        protected object target;
        public event EventHandler OnAbilityInterrupted;
        public event EventHandler<AbilityStatusEventArgs> OnPreAbilityActivation;
        public event EventHandler<AbilityStatusEventArgs> OnPostAbilityActivation;

        public event EventHandler<OldNewEventArgs<float>> OnCooldownChanged;
        public event EventHandler<OldNewEventArgs<int>> OnChargesChanged;

        public virtual void Bind([NotNull] IAbility abilityHandler)
        {
            Bind(abilityHandler, null);
        }

        protected void Bind([NotNull] IAbility abilityHandler, IAbilityTargeter abilityTargeter)
        {
            AbilityTargeter = abilityTargeter;
            abilityManager = abilityHandler;
            abilityStatusEventArgs = new AbilityStatusEventArgs(this);
            switch (AbilityActivationType)
            {
                case AbilityActivationType.Instant:
                    AbilityActivator = new InstantAbilityActivator(PerformAbility);
                    break;
                case AbilityActivationType.Cast:
                    AbilityActivator =
                        new CastAbilityActivator(PerformAbility, CastTime, abilityManager as MonoBehaviour);
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

            if (AbilityTargeter != null)
            {
                if (AbilityTargeter.RequiresSeperateTargeting && !AbilityTargeter.IsTargeting)
                {
                    AbilityTargeter.StartTargeting();
                    return;
                }
                else
                {
                    target = AbilityTargeter.GetTarget();
                }
            }

            OnPreAbilityActivation?.Invoke(this, abilityStatusEventArgs);
            AbilityActivator.Activate();
        }

        protected virtual void PerformAbility()
        {
            OnPostAbilityActivation?.Invoke(this, abilityStatusEventArgs);
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

        public void Interrupt()
        {
            AbilityActivator.Interrupt();
            OnAbilityInterrupted?.Invoke(this, null);
        }
    }

    public interface IAbilityTargeter
    {
        bool IsTargeting { get; }
        bool RequiresSeperateTargeting { get; }

        /*
        IAbilitiTargeter(ref object target);
*/
        void StartTargeting();
        object GetTarget();
    }
}