using System;

namespace SejDev.Systems.StatusEffects
{
    public class StatusEffectChangedEventArgs : EventArgs
    {
        public int? currentStacks;
        public float durationLeft;
        public StatusEffect statusEffect;

        public StatusEffectChangedEventArgs(int currentStacks)
        {
            this.currentStacks = currentStacks;
        }

        public StatusEffectChangedEventArgs(StatusEffect statusEffect)
        {
            this.statusEffect = statusEffect;
        }

        public StatusEffectChangedEventArgs(float durationLeft)
        {
            this.durationLeft = durationLeft;
        }
    }
}