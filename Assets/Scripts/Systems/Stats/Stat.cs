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
            float absoluteNormal = modContainer.GetFinalModifierByType(ModifierType.Absolute);
            float absoluteOverriding = modContainer.GetFinalOverridingModifierByType(ModifierType.Absolute);
            float percentNormal = modContainer.GetFinalModifierByType(ModifierType.Percent);
            float percentOverriding = modContainer.GetFinalOverridingModifierByType(ModifierType.Percent);

            bool sameSign = (absoluteNormal >= 0 && absoluteOverriding >= 0) ||
                            (absoluteNormal < 0 && absoluteOverriding < 0);
            bool absOverrideSmaller = Mathf.Abs(absoluteOverriding) < Mathf.Abs(absoluteNormal);

            newValue += absoluteNormal;
            if (restrictor != null && sameSign)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                baseValue * restrictor.maxPercent);
            }

            newValue += absoluteOverriding; //final value for sameSign / differentSign+overridebigger
            if (restrictor != null && !sameSign && absOverrideSmaller)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }
            
            // if (sameSign)
            // {
            //     //same sign
            //     newValue += absoluteNormal;
            //     if (restrictor != null)
            //     {
            //         newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
            //             baseValue * restrictor.maxPercent);
            //     }
            //
            //     newValue += absoluteOverriding;
            //     //adhere to restrictions
            // }
            // else
            // {
            //     //different sign 
            //     if (overrideSmaller)
            //     {
            //         //override smaller
            //         newValue += absoluteOverriding;
            //         newValue += absoluteNormal;
            //     }
            //     else
            //     {
            //         
            //     }
            // }

            // //add absolute value to base
            // newValue += modContainer.GetFinalModifierByType(ModifierType.Absolute);
            // //apply multiplier
            // newValue *= 1 + modContainer.GetFinalModifierByType(ModifierType.Percent);
            // if (restrictor != null)
            //     //adhere to restrictions
            //     newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
            //         baseValue * restrictor.maxPercent);
            currentValue = newValue;
        }

        public void RemoveModifier(Modifier modifier)
        {
            modContainer.Modifiers.Remove(modifier);
            RaiseOnStatChanged();
        }
    }

    public class ModifierEvalutationTestImpl
    {
        public float Evaluate(float baseValue, float absoluteNormal, float absoluteOverriding, StatRestrictor restrictor)
        {
            var newValue = baseValue;

            bool sameSign = (absoluteNormal >= 0 && absoluteOverriding >= 0) ||
                            (absoluteNormal < 0 && absoluteOverriding < 0);
            bool absOverrideSmaller = Mathf.Abs(absoluteOverriding) < Mathf.Abs(absoluteNormal);

            newValue += absoluteNormal;
            if (restrictor != null && sameSign)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }

            newValue += absoluteOverriding; //final value for sameSign / differentSign+overridebigger
            if (restrictor != null && !sameSign && absOverrideSmaller)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }

            return newValue;
        }
    }
}