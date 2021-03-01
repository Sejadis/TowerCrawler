using System;
using System.Collections.Generic;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using UnityEngine;

namespace SejDev.Systems.Stats
{
    [CreateAssetMenu(fileName = "Assets/Resources/Items/NewRuleSet",
        menuName = "Systems/Equipment/RuleSet")]
    [Serializable]
    public class RuleSet : ScriptableObject
    {
        [SerializeField] private SlotRarityDistributionData slotRarityDistributionData;

        [EnumNamedArray(typeof(StatType))] [SerializeField]
        private StatValueDistributionData[] statValueDistributionData =
            new StatValueDistributionData[Enum.GetNames(typeof(StatType)).Length];

        [EnumNamedArray(typeof(EquipSlotType))] [SerializeField]
        private GuaranteedStatSlotData[] guaranteedSlotStatData =
            new GuaranteedStatSlotData[Enum.GetNames(typeof(EquipSlotType)).Length];

        private void OnValidate()
        {
            slotRarityDistributionData.Validate();

            var enumLength = Enum.GetNames(typeof(StatType)).Length;
            if (statValueDistributionData.Length != enumLength)
            {
                var array = new StatValueDistributionData[enumLength];
                var copyLength = statValueDistributionData.Length < array.Length
                    ? statValueDistributionData.Length
                    : array.Length;
                Array.Copy(statValueDistributionData, array, copyLength);
            }

            enumLength = Enum.GetNames(typeof(EquipSlotType)).Length;
            if (guaranteedSlotStatData.Length != enumLength)
            {
                var array = new GuaranteedStatSlotData[enumLength];
                var copyLength = guaranteedSlotStatData.Length < array.Length
                    ? guaranteedSlotStatData.Length
                    : array.Length;
                Array.Copy(guaranteedSlotStatData, array, copyLength);
            }
        }

        public List<GuaranteedStatData> GetGuaranteedStats(EquipSlotType slotType)
        {
            return guaranteedSlotStatData[Utility.GetValueIndex(slotType)].guaranteedStatData;
        }

        public MinMaxDistributionData GetStatRules(StatType statType, Rarity rarity)
        {
            var typeValue = Utility.GetValueIndex(statType);
            var rarityValue = Utility.GetValueIndex(rarity);
            return statValueDistributionData[typeValue].distributionData[rarityValue];
        }

        public ModifierType GetModifierType(StatType statType)
        {
            return statValueDistributionData[Utility.GetValueIndex(statType)].modifierType;
        }

        public List<StatType> GetPossibleStats(Rarity rarity)
        {
            var result = new List<StatType>();
            var statTypeLength = Enum.GetNames(typeof(StatType)).Length;
            for (int i = 0; i < statTypeLength; i++)
            {
                var valueData = statValueDistributionData[i].distributionData[Utility.GetValueIndex(rarity)];
                if (!valueData.IsEmpty())
                {
                    result.Add((StatType) (1 << i));
                }
            }

            return result;
        }

        public MinMaxData GetStatAmount(EquipSlotType equipSlot, Rarity rarity)
        {
            return slotRarityDistributionData.distributionData[Utility.GetValueIndex(equipSlot)]
                .distributionData[Utility.GetValueIndex(rarity)];
        }

        public ValueType GetValueType(StatType statType)
        {
            return statValueDistributionData[Utility.GetValueIndex(statType)].valueType;
        }
    }
}