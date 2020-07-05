using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Stats
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Stats/NewStat", menuName = "Systems/Stats/Stat")]
    public class Stat : ScriptableObject
    {
        [SerializeField] protected float baseValue;

        private float? currentValue;

        private readonly ModifierContainer modContainer = new ModifierContainer();

        [SerializeField] protected StatRestrictor restrictor;

        [field: Rename]
        [field: SerializeField]
        public StatType Type { get; protected set; }

        public float Value
        {
            get
            {
                if (currentValue == null) Evaluate();

                return (float) currentValue;
            }
        }

        public event EventHandler<StatChangedEventArgs> OnStatChanged;

        public void AddModifier(Modifier modifier)
        {
            modContainer.Modifiers.Add(modifier);
            RaiseOnStatChanged();
        }

        protected void RaiseOnStatChanged()
        {
            var oldValue = Value;
            Evaluate();
            var args = new StatChangedEventArgs(oldValue, Value);
            OnStatChanged?.Invoke(this, args);
        }

        private void Evaluate()
        {
            float newValue;
            newValue = baseValue;

            //add absolute value to base
            newValue += modContainer.GetFinalModifierByType(ModifierType.Absolute);
            //apply multiplier
            newValue *= 1 + modContainer.GetFinalModifierByType(ModifierType.Percent);
            if (restrictor != null)
                //adhere to restrictions
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            currentValue = newValue;
        }

        public void RemoveModifier(Modifier modifier)
        {
            modContainer.Modifiers.Remove(modifier);
            RaiseOnStatChanged();
        }
    }
}