using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.StatusEffects
{
    [Serializable]
    public abstract class StatusEffect : ScriptableObject, IEquatable<StatusEffect>
    {
        protected float applyTime;
        private float durationLeft;
        protected Guid guid = Guid.NewGuid();


        protected StatusEffectManager statusEffectManager;

        [field: SerializeField]
        [field: Rename]
        public EffectType EffectType { get; protected set; }

        [field: SerializeField]
        [field: Rename]
        public string Name { get; protected set; }

        [field: SerializeField]
        [field: Rename]
        public bool IsExclusive { get; protected set; }

        [field: SerializeField]
        [field: Rename]
        public float Duration { get; protected set; }

        public float DurationLeft
        {
            get => durationLeft;
            protected set
            {
                if (value == durationLeft) return;

                durationLeft = value;
                RaiseOnStatusEffectChanged(new StatusEffectChangedEventArgs(DurationLeft));
            }
        }

        public bool Equals(StatusEffect other)
        {
            return base.Equals(other) && guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StatusEffect) obj);
        }

        public event EventHandler<StatusEffectChangedEventArgs> OnStatusEffectChanged;

        protected void RaiseOnStatusEffectChanged(StatusEffectChangedEventArgs statusEffectChangedEventArgs)
        {
            OnStatusEffectChanged?.Invoke(this, statusEffectChangedEventArgs);
        }

        public virtual void Bind(StatusEffectManager statusEffectManager)
        {
            this.statusEffectManager = statusEffectManager;
            DurationLeft = Duration;
            applyTime = Time.time;
        }

        public virtual void UnBind()
        {
        }

        public void UpdateDuration(float deltaTime)
        {
            DurationLeft -= deltaTime;
            if (DurationLeft <= 0) RemoveSelf();
        }

        protected void RemoveSelf()
        {
            statusEffectManager?.RemoveStatusEffect(this);
        }

        public bool HasSameBaseObject(StatusEffect other)
        {
            return other.guid.Equals(guid);
        }

        public StatusEffect CreateDeepClone()
        {
            var clone = Instantiate(this);
            clone.guid = guid;
            return clone;
        }
    }
}