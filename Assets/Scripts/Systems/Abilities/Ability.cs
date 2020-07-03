using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        [field: Rename, SerializeField] public string Name { get; protected set; }
        [field: Rename, SerializeField] public Sprite Icon { get; protected set; }
        [field: Rename, SerializeField] public float Cooldown { get; protected set; }
        [field: Rename, SerializeField] public float RemainingCooldown { get; protected set; }
        
        public event EventHandler<OldNewEventArgs<float>> OnCooldownChanged;
        private AbilityManager abilityManager;
        
        public virtual void Bind(AbilityManager abilityManager)
        {
            this.abilityManager = abilityManager;
        }

        public virtual void Activate()
        {
            RemainingCooldown = Cooldown;
        }

        public void UpdateCooldown(float deltaTime)
        {
            if(RemainingCooldown < 0) return;
            
            float old = RemainingCooldown;
            RemainingCooldown -= deltaTime;
            OnCooldownChanged?.Invoke(this,new OldNewEventArgs<float>(old,RemainingCooldown));
        }
    }
}