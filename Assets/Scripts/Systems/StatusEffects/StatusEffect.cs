using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.StatusEffects
{
    [Serializable]
    public abstract class StatusEffect : ScriptableObject, IEquatable<StatusEffect>
    {
        public bool Equals(StatusEffect other)
        {
            return base.Equals(other) && guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StatusEffect) obj);
        }

        [field: SerializeField, Rename] public EffectType EffectType { get; protected set; }
        [field: SerializeField, Rename] public string Name { get; protected set; }
        [field: SerializeField, Rename] public bool IsExclusive { get; protected set; }
        [field: SerializeField, Rename] public float Duration { get; protected set; }

        protected float applyTime;
        protected Guid guid = Guid.NewGuid();
        private float durationLeft;

        public float DurationLeft
        {
            get { return durationLeft; }
            protected set
            {
                if (value == durationLeft)
                {
                    return;
                }

                durationLeft = value;
                RaiseOnStatusEffectChanged(new StatusEffectChangedEventArgs(DurationLeft));
            }
        }


        protected StatusEffectManager statusEffectManager;
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
            if (DurationLeft <= 0)
            {
                RemoveSelf();
            }
        }

        protected void RemoveSelf()
        {
            statusEffectManager?.RemoveStatusEffect(this);
        }

        public bool HasSameBaseObject(StatusEffect other)
        {
            return other.guid.Equals(this.guid);
        }

        public StatusEffect CreateDeepClone()
        {
            StatusEffect clone = Instantiate(this);
            clone.guid = guid;
            return clone;
        }
    }
}