using System.Collections.Generic;
using SejDev.Systems.StatusEffects;
using UnityEngine;

namespace SejDev.StatusEffects.Test
{
    public class StatusEffectTrigger : MonoBehaviour
    {
        public Dictionary<IBuffable, List<Systems.StatusEffects.StatusEffect>> affectedTargets = new Dictionary<IBuffable, List<Systems.StatusEffects.StatusEffect>>();
        public Systems.StatusEffects.StatusEffect effect;
        private void OnTriggerEnter(Collider other)
        {

            var buffable = other.gameObject.GetComponent<IBuffable>();
            if (buffable != null)
            {
                if (effect is StackableStatusEffect)
                {
                    buffable.AddStatusEffect(effect as StackableStatusEffect, 1);
                }
                else
                {
                    Systems.StatusEffects.StatusEffect s = buffable.AddStatusEffect(effect);
                    if (effect.EffectType.Equals(EffectType.Trigger))
                    {
                        if(!affectedTargets.ContainsKey(buffable))
                        {
                            affectedTargets[buffable] = new List<Systems.StatusEffects.StatusEffect>();
                        }
                    
                        affectedTargets[buffable].Add(s);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var buffable = other.gameObject.GetComponent<IBuffable>();
            if (buffable != null && affectedTargets.ContainsKey(buffable))
            {
                foreach (var effect in affectedTargets[buffable])
                {
                    buffable.RemoveStatusEffect(effect);
                }
            }
        }
    }
}
