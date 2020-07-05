using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.StatusEffects
{
    public abstract class StackableStatusEffect : StatusEffect
    {
        protected int currentStacks;

        [field: SerializeField]
        [field: Rename]
        public bool StacksRefreshDuration { get; protected set; }

        [field: SerializeField]
        [field: Rename]
        [field: Min(0)]
        public int MaxStacks { get; protected set; }

        public int CurrentStacks
        {
            get => currentStacks;
            protected set
            {
                if (value == currentStacks) return;

                if (value == 0) RemoveSelf();

                currentStacks = Mathf.Min(value, MaxStacks);
                RaiseOnStatusEffectChanged(new StatusEffectChangedEventArgs(CurrentStacks));
            }
        }

        public abstract void Bind(StatusEffectManager statusEffectManager, int stacks);

        public void AddStack(int amount)
        {
            var old = CurrentStacks;
            CurrentStacks += amount;

            if (CurrentStacks != old)
                if (StacksRefreshDuration)
                    DurationLeft = Duration;
        }
    }
}