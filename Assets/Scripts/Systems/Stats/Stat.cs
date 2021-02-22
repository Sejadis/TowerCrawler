using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Stats
{
    [CreateAssetMenu(fileName = "Assets/Resources/Stats/NewStat", menuName = "Systems/Stats/Stat")]
    public class Stat : ScriptableObject
    {
        [SerializeField] protected float baseValue;

        private float? currentValue = null;

        private readonly ModifierContainer modContainer = new ModifierContainer();

        [SerializeField] protected StatRestrictor restrictor;

        [field: Rename]
        [field: SerializeField]
        public StatType Type { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public DisplayStyle DisplayStyle { get; protected set; }

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
            var newValue = baseValue;
            float absoluteNormal = modContainer.GetFinalModifierByType(ModifierType.Absolute);
            float absoluteOverriding = modContainer.GetFinalOverridingModifierByType(ModifierType.Absolute);

            bool sameSignAbsolute = (absoluteNormal >= 0 && absoluteOverriding >= 0) ||
                                    (absoluteNormal < 0 && absoluteOverriding < 0);
            bool absOverrideSmaller = Mathf.Abs(absoluteOverriding) < Mathf.Abs(absoluteNormal);

            newValue += absoluteNormal;
            if (restrictor != null && sameSignAbsolute)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }

            newValue += absoluteOverriding; //final value for sameSign / differentSign+overridebigger
            if (restrictor != null && !sameSignAbsolute && absOverrideSmaller)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }


            float percentNormal = modContainer.GetFinalModifierByType(ModifierType.Percent);
            float percentOverriding = modContainer.GetFinalOverridingModifierByType(ModifierType.Percent);
            bool sameSignPercent = (percentNormal >= 0 && percentOverriding >= 0) ||
                                   (percentNormal < 0 && percentOverriding < 0);
            bool percentOverrideSmaller = Mathf.Abs(percentOverriding) < Mathf.Abs(percentNormal);

            newValue *= 1 + percentNormal;
            if (restrictor != null && sameSignPercent)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }

            newValue *= 1 + percentOverriding; //final value for sameSign / differentSign+overridebigger
            if (restrictor != null && !sameSignPercent && percentOverrideSmaller)
            {
                newValue = Mathf.Clamp(newValue, baseValue * restrictor.minPercent,
                    baseValue * restrictor.maxPercent);
            }

            currentValue = newValue;
        }

        public void RemoveModifier(Modifier modifier)
        {
            modContainer.Modifiers.Remove(modifier);
            RaiseOnStatChanged();
        }

        public override string ToString()
        {
            //{(modifier.type == ModifierType.Percent ? "%" : "")} 
            string value = String.Empty;
            switch (DisplayStyle)
            {
                case DisplayStyle.Basic:
                    value = Value.ToString();
                    break;
                case DisplayStyle.PercentToBaseValue:
                    value = $"{((Value / baseValue) * 100f).ToString()}%";
                    break;
            }

            return
                $"{value} {Enum.GetName(typeof(StatType), value: Type)}";
        }
    }

    public enum DisplayStyle
    {
        Basic,
        PercentToBaseValue
    }

    public class ModifierEvalutationTestImpl
    {
        public float Evaluate(float baseValue, float absoluteNormal, float absoluteOverriding,
            StatRestrictor restrictor)
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