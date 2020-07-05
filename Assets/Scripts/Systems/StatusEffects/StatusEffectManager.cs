using System;
using System.Collections.Generic;
using UnityEngine;

namespace SejDev.Systems.StatusEffects
{
    public class StatusEffectManager : MonoBehaviour, IBuffable
    {
        private readonly List<StatusEffect> statusEffects = new List<StatusEffect>();

        public event EventHandler<StatusEffectChangedEventArgs> OnStatusEffectAdded;
        public event EventHandler<StatusEffectChangedEventArgs> OnStatusEffectRemoved;

        public StatusEffect AddStatusEffect(StatusEffect statusEffect)
        {
            //StatusEffect effect = ScriptableObject.CreateInstance(statusEffect.GetType()) as StatusEffect;
            var effect = statusEffect.CreateDeepClone();
            //effect.SetFrom(statusEffect);
            //Debug.Log($"Original {statusEffect.GetType()}  instanceaw {effect.GetType()}");
            //Debug.Log((effect as DamageMitigationStatusEffect).DamageMitigationPercent);
            if (effect.IsExclusive && ContainsEffect(effect))
                //same instance of a status effect is not allowed
                return null;
            // if(statusEffect.IsExclusive && ContainsType(statusEffect.GetType())){
            //     //different instance of a status effect but effect is exclusive
            //     return;
            // }

            statusEffects.Add(effect);
            effect.Bind(this);
            OnStatusEffectAdded?.Invoke(this, new StatusEffectChangedEventArgs(effect));
            return effect;
        }

        public StackableStatusEffect AddStatusEffect(StackableStatusEffect statusEffect, int stacks = 1)
        {
            var effect = (StackableStatusEffect) statusEffect.CreateDeepClone();

            var existingEffect = ContainsEffect(effect);
            if (existingEffect != null)
            {
                if (existingEffect.IsExclusive)
                    //same instance of a status effect is not allowed
                    return null;

                (existingEffect as StackableStatusEffect).AddStack(stacks);
            }
            else
            {
                statusEffects.Add(effect);
                effect.Bind(this, stacks);
                OnStatusEffectAdded?.Invoke(this, new StatusEffectChangedEventArgs(effect));
            }

            return effect;
        }

        public void RemoveStatusEffect(StatusEffect statusEffect)
        {
            if (statusEffects.Contains(statusEffect))
            {
                statusEffect.UnBind();
                statusEffects.Remove(statusEffect);
                OnStatusEffectRemoved?.Invoke(this, new StatusEffectChangedEventArgs(statusEffect));
            }
        }

        private void Update()
        {
            for (var i = statusEffects.Count - 1; i >= 0; i--)
                if (statusEffects[i].EffectType.Equals(EffectType.Duration))
                    statusEffects[i].UpdateDuration(Time.deltaTime);
        }

        private StatusEffect ContainsEffect(StatusEffect statusEffect)
        {
            foreach (var effect in statusEffects)
                if (effect.HasSameBaseObject(statusEffect))
                    return effect;

            return null;
        }
    }
}