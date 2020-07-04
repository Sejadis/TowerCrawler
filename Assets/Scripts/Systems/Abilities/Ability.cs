using System;
using SejDev.Abilities.Activator;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        [field: Rename, SerializeField] public string Name { get; protected set; }
        [field: Rename, SerializeField] public Sprite Icon { get; protected set; }
        [field: Rename, SerializeField] public AbilityType Type { get; protected set; }
        [field: Rename, SerializeField] public float Cooldown { get; protected set; }
        //TODO energy drain, max energy, energy rate
        //TODO charge, charge cd
        
        [field: Rename, SerializeField] public AbilityActivationType AbilityActivationType { get; protected set; }
        [field: Rename, SerializeField] public float CastTime { get; protected set; }
        [field: Rename, SerializeField] public float ChannelTime { get; protected set; }
        public event EventHandler<AbilityActivationEventArgs> OnPreAbilityActivation;
        public event EventHandler<AbilityActivationEventArgs> OnPostAbilityActivation;

        public bool CanActivate => RemainingCooldown <= 0 && !AbilityActivator.IsActive;
        public float RemainingCooldown { get; protected set; }
        public IAbilityActivator AbilityActivator { get; protected set; }
        private AbilityActivationEventArgs abilityActivationEventArgs;
        
        public event EventHandler<OldNewEventArgs<float>> OnCooldownChanged;
        private AbilityManager abilityManager;
        
        public virtual void Bind(AbilityManager abilityManager)
        {
            this.abilityManager = abilityManager;
            abilityActivationEventArgs = new AbilityActivationEventArgs(this);
            switch (AbilityActivationType)
            {
                case AbilityActivationType.Instant:
                    AbilityActivator = new InstantAbilityActivator(PerformAbility);
                    break;
                case AbilityActivationType.Cast:
                    AbilityActivator = new CastAbilityActivator(PerformAbility,CastTime,abilityManager);
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
        }

        public void Activate()
        {
            // Debug.Log($"call to activate (will be {CanActivate})");
            if(!CanActivate) return;
            OnPreAbilityActivation?.Invoke(this, abilityActivationEventArgs);
            AbilityActivator.Activate();
        }

        public void UpdateCooldown(float deltaTime)
        {
            if(RemainingCooldown < 0) return;
            
            float old = RemainingCooldown;
            RemainingCooldown -= deltaTime;
            OnCooldownChanged?.Invoke(this,new OldNewEventArgs<float>(old,RemainingCooldown));
        }

        protected virtual void PerformAbility()
        {
            OnPostAbilityActivation?.Invoke(this, abilityActivationEventArgs);
            RemainingCooldown = Cooldown;
        }
    }

    public class ChannelAbilityActivator : IAbilityActivator
    {
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public bool IsActive { get; }
        public event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }

    public class CastChannelAbilityActivator : IAbilityActivator
    {
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public bool IsActive { get; }
        public event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
    }
}