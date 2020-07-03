using System;

namespace SejDev.Systems.StatusEffects
{
    public interface IBuffable
    {
        event EventHandler<StatusEffectChangedEventArgs> OnStatusEffectAdded;
        event EventHandler<StatusEffectChangedEventArgs> OnStatusEffectRemoved;
        StatusEffect AddStatusEffect(StatusEffect statusEffect);
        StackableStatusEffect AddStatusEffect(StackableStatusEffect statusEffect, int stacks);
        void RemoveStatusEffect(StatusEffect statusEffect);
    }
}