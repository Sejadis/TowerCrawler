using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Stats
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Stats/NewStat", menuName = "Systems/Stats/Stat")]
    public class Stat : ScriptableObject
    {
        [field: Rename, SerializeField] public StatType Type { get; protected set; }

        [SerializeField] protected StatRestrictor restrictor;

        [SerializeField] protected float baseValue;

        private float? currentValue = null;

        public event EventHandler<StatChangedEventArgs> OnStatChanged;

        private ModifierContainer modContainer = new ModifierContainer();

        public float Value
        {
            get
            {
                if (currentValue == null)
                {
                    Evaluate();
                }

                return (float) currentValue;
            }
        }

        public void AddModifier(Modifier modifier)
        {
            modContainer.Modifiers.Add(modifier);
            RaiseOnStatChanged();
        }

        protected void RaiseOnStatChanged()
        {
            float oldValue = Value;
            Evaluate();
            StatChangedEventArgs args = new StatChangedEventArgs(oldValue,Value);
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
            {
                //adhere to restrictions
                newValue = Mathf.Clamp((float) newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }
            currentValue = newValue;
        }

        public void RemoveModifier(Modifier modifier)
        {
            modContainer.Modifiers.Remove(modifier);
            RaiseOnStatChanged();
        }
    }
}