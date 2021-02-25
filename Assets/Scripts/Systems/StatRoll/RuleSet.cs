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
                // for (int i = 0; i < statValueDistributionData.Length && i < enumLength; i++)
                // {
                //     array[i] = statValueDistributionData[i];
                // }
                //
                // statValueDistributionData = array;
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
            return guaranteedSlotStatData[(int) slotType].guaranteedStatData;
        }

        public MinMaxDistributionData GetStatRules(StatType statType, Rarity rarity)
        {
            return statValueDistributionData[(int) statType].distributionData[(int) rarity];
        }

        public ModifierType GetModifierType(StatType statType)
        {
            return statValueDistributionData[(int) statType].modifierType;
        }
    }
}