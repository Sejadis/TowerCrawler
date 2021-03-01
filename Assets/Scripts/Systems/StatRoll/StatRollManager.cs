using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using UnityEngine;

namespace SejDev.Systems.Stats
{
    public static class StatRollManager
    {
        private static RuleSet ruleSet;

        public static void Initialize()
        {
            ruleSet = Resources.Load<RuleSet>("Items/DefaultRuleSet");
        }

        public static Equipment.Equipment RollStats(Equipment.Equipment item)
        {
            //roll rarity
            Rarity rarity = RollRarity();
            return RollStats(item, rarity);
        }

        public static Equipment.Equipment RollStats(Equipment.Equipment item, Rarity rarity)
        {
            var eq = item.CreateDeepClone();
            eq.rarity = rarity;

            var guaranteedStatDataSlot = ruleSet.GetGuaranteedStats(eq.EquipSlot);
            var guaranteedStatDataEquipment = item.GuaranteedStats;
            var guaranteedStatData = MergeGuaranteedStats(guaranteedStatDataEquipment, guaranteedStatDataSlot);
            var possibleStats = ruleSet.GetPossibleStats(eq.rarity);
            var statAmountData = ruleSet.GetStatAmount(eq.EquipSlot, eq.rarity);
            //TODO create a int version of MinMaxData to avoid rounding and because
            //there cant be half a stat on an item
            var statAmount = Mathf.RoundToInt(Random.Range(statAmountData.minValue, statAmountData.maxValue));
            var rolledStats = new List<StatType>();
            var stats = new List<EquipmentStat>();
            foreach (var data in guaranteedStatData)
            {
                possibleStats.Remove(data.statType);
                if (!data.overrideData.IsEmpty())
                {
                    var stat = RollStat(eq, data.statType, data.overrideData);
                    stats.Add(stat);
                    if (data.countsForTotalStats)
                    {
                        statAmount--;
                    }
                }
                else
                {
                    //we dont have override data for the given stat type, so we roll it later
                    //and remove it from the list of stats that can still be rolled
                    rolledStats.Add(data.statType);
                    //
                    if (!data.countsForTotalStats)
                    {
                        statAmount++;
                    }
                }
            }

            while (rolledStats.Count < statAmount)
            {
                var rndValue = Random.Range(0, possibleStats.Count);
                rolledStats.Add(possibleStats[rndValue]);
                possibleStats.RemoveAt(rndValue);
            }

            foreach (var statType in rolledStats)
            {
                var stat = RollStat(eq, statType);
                stats.Add(stat);
            }

            eq.stats = stats;
            return eq;
        }

        private static EquipmentStat RollStat(Equipment.Equipment eq, StatType statType,
            MinMaxDistributionData overrideData = null)
        {
            var roll = RollStatValue(statType, eq.rarity, overrideData);
            var modType = ruleSet.GetModifierType(statType);
            if (modType == ModifierType.Percent)
            {
                //convert display value to value based on 1 as 100% (i.e. 2.5% -> 0.025)
                roll /= 100;
            }

            Modifier mod;
            if (ruleSet.GetValueType(statType) == ValueType.Integer)
            {
                mod = new Modifier(modType, Mathf.CeilToInt(roll), eq);
            }
            else
            {
                mod = new Modifier(modType, roll, eq);
            }

            var stat = new EquipmentStat(statType, mod);
            return stat;
        }

        /// <summary>
        /// Merges guaranteed stats from the item and the slot
        /// IMPORTANT: order of parameters matter as override stats from the Item are prioritized
        /// </summary>
        /// <param name="dataSetItem"></param>
        /// <param name="dataSetSlot"></param>
        /// <returns></returns>
        private static List<GuaranteedStatData> MergeGuaranteedStats(List<GuaranteedStatData> dataSetItem,
            List<GuaranteedStatData> dataSetSlot)
        {
            List<GuaranteedStatData> result = new List<GuaranteedStatData>();
            foreach (var data in dataSetSlot)
            {
                result.Add(data);
            }

            foreach (var data in dataSetItem)
            {
                //check if we already have something of that stat type in the list
                var slotData = result.FirstOrDefault(s => s.statType == data.statType);
                if (slotData != null)
                {
                    //we do, so remove the existing one and replace it by the new one as item stats have priority over slot stats
                    result.Remove(slotData);
                    result.Add(data);
                }
            }

            return result;
        }

        private static float RollStatValue(StatType statType, Rarity rarity, MinMaxDistributionData overrideData = null)
        {
            //get data from ruleset if no override data 
            var statRules = overrideData ?? ruleSet.GetStatRules(statType, rarity);
            var rollValue = statRules.Evaluate();

            //round to 1 decimal place
            rollValue *= 10f;
            rollValue = Mathf.Round(rollValue);
            rollValue /= 10f;

            return rollValue;
        }

        private static Rarity RollRarity()
        {
            Rarity rarity;
            switch (Random.Range(0f, 1f))
            {
                case var v when v >= 0.95f:
                    rarity = Rarity.Legendary;
                    break;
                case var v when v >= 0.85f:
                    rarity = Rarity.Epic;
                    break;
                case var v when v >= 0.7f:
                    rarity = Rarity.Rare;
                    break;
                case var v when v >= 0.4f:
                    rarity = Rarity.Uncommon;
                    break;
                default:
                    rarity = Rarity.Common;
                    break;
            }

            return rarity;
        }
    }
}