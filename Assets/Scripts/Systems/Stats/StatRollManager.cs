using System;
using System.Collections.Generic;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SejDev.Systems.Stats
{
    public static class StatRollManager
    {
        private static Dictionary<(StatType, Rarity), StatRuleSet> statRuleSets =
            new Dictionary<(StatType, Rarity), StatRuleSet>();

        private static Dictionary<(EquipSlotType, Rarity), SlotRuleSet> slotRuleSets =
            new Dictionary<(EquipSlotType, Rarity), SlotRuleSet>();

        public static void Initialize()
        {
            InitStatRuleSets();
            InitSlotRuleSets();
        }

        private static void InitSlotRuleSets()
        {
            slotRuleSets[(EquipSlotType.Weapon, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Weapon, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Weapon, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Weapon, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Weapon, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Head, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Head, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Head, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Head, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Head, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Shoulder, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Shoulder, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Shoulder, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Shoulder, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Shoulder, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Chest, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Chest, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Chest, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Chest, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Chest, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Arms, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Arms, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Arms, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Arms, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Arms, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Hands, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Hands, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Hands, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Hands, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Hands, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Legs, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Legs, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Legs, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Legs, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Legs, Rarity.Legendary)] = new SlotRuleSet(5, 5);

            slotRuleSets[(EquipSlotType.Feet, Rarity.Common)] = new SlotRuleSet(1, 1);
            slotRuleSets[(EquipSlotType.Feet, Rarity.Uncommon)] = new SlotRuleSet(1, 2);
            slotRuleSets[(EquipSlotType.Feet, Rarity.Rare)] = new SlotRuleSet(2, 4);
            slotRuleSets[(EquipSlotType.Feet, Rarity.Epic)] = new SlotRuleSet(3, 5);
            slotRuleSets[(EquipSlotType.Feet, Rarity.Legendary)] = new SlotRuleSet(5, 5);
        }

        private static void InitStatRuleSets()
        {
            statRuleSets[(StatType.Damage, Rarity.Common)] = new StatRuleSet(ModifierType.Absolute, 1, 10);
            statRuleSets[(StatType.Damage, Rarity.Uncommon)] = new StatRuleSet(ModifierType.Absolute, 2, 10);
            statRuleSets[(StatType.Damage, Rarity.Rare)] = new StatRuleSet(ModifierType.Absolute, 5, 10);
            statRuleSets[(StatType.Damage, Rarity.Epic)] = new StatRuleSet(ModifierType.Absolute, 5, 15);
            statRuleSets[(StatType.Damage, Rarity.Legendary)] = new StatRuleSet(ModifierType.Absolute, 10, 15);

            statRuleSets[(StatType.CastTime, Rarity.Common)] = new StatRuleSet(ModifierType.Percent, 0.005f, 0.03f);
            statRuleSets[(StatType.CastTime, Rarity.Uncommon)] = new StatRuleSet(ModifierType.Percent, 0.01f, 0.03f);
            statRuleSets[(StatType.CastTime, Rarity.Rare)] = new StatRuleSet(ModifierType.Percent, 0.015f, 0.03f);
            statRuleSets[(StatType.CastTime, Rarity.Epic)] = new StatRuleSet(ModifierType.Percent, 0.025f, 0.035f);
            statRuleSets[(StatType.CastTime, Rarity.Legendary)] = new StatRuleSet(ModifierType.Percent, 0.03f, 0.035f);

            statRuleSets[(StatType.MidAirJump, Rarity.Common)] = null; //cant roll on this rarity
            statRuleSets[(StatType.MidAirJump, Rarity.Uncommon)] = null;
            statRuleSets[(StatType.MidAirJump, Rarity.Rare)] = null;
            statRuleSets[(StatType.MidAirJump, Rarity.Epic)] = new StatRuleSet(ModifierType.Absolute, 1, 1);
            statRuleSets[(StatType.MidAirJump, Rarity.Legendary)] = new StatRuleSet(ModifierType.Absolute, 1, 1);

            statRuleSets[(StatType.CooldownRate, Rarity.Common)] = new StatRuleSet(ModifierType.Percent, 0.005f, 0.01f);
            statRuleSets[(StatType.CooldownRate, Rarity.Uncommon)] =
                new StatRuleSet(ModifierType.Percent, 0.005f, 0.015f);
            statRuleSets[(StatType.CooldownRate, Rarity.Rare)] = new StatRuleSet(ModifierType.Percent, 0.01f, 0.015f);
            statRuleSets[(StatType.CooldownRate, Rarity.Epic)] = new StatRuleSet(ModifierType.Percent, 0.01f, 0.025f);
            statRuleSets[(StatType.CooldownRate, Rarity.Legendary)] =
                new StatRuleSet(ModifierType.Percent, 0.015f, 0.025f);

            statRuleSets[(StatType.MovementSpeed, Rarity.Common)] = null;
            statRuleSets[(StatType.MovementSpeed, Rarity.Uncommon)] =
                new StatRuleSet(ModifierType.Percent, 0.005f, 0.025F);
            statRuleSets[(StatType.MovementSpeed, Rarity.Rare)] = new StatRuleSet(ModifierType.Percent, 0.01f, 0.03F);
            statRuleSets[(StatType.MovementSpeed, Rarity.Epic)] = new StatRuleSet(ModifierType.Percent, 0.02f, 0.03F);
            statRuleSets[(StatType.MovementSpeed, Rarity.Legendary)] =
                new StatRuleSet(ModifierType.Percent, 2.5f, 3.5F);
        }

        public static Equipment.Equipment RollStats(Equipment.Equipment item)
        {
            var eq = item.CreateDeepClone();
            //roll rarity
            switch (Random.Range(0f, 1f))
            {
                case var v when v >= 0.95f:
                    eq.rarity = Rarity.Legendary;
                    break;
                case var v when v >= 0.85f:
                    eq.rarity = Rarity.Epic;
                    break;
                case var v when v >= 0.7f:
                    eq.rarity = Rarity.Rare;
                    break;
                case var v when v >= 0.4f:
                    eq.rarity = Rarity.Uncommon;
                    break;
                default:
                    eq.rarity = Rarity.Common;
                    break;
            }

            var stats = new List<EquipmentStat>();
            if (slotRuleSets.TryGetValue((eq.EquipSlot, eq.rarity), out var slotRuleSet))
            {
                //how many stats will be on the item
                var statCount = Random.Range(slotRuleSet.minStatCount, slotRuleSet.maxStatCount + 1);
                //which stats can we have with this rarity
                var possibleTypes = GetPossibleStatTypes(eq.rarity);
                //we dont have enough stats yet and still possible stats left to choose from
                while (stats.Count < statCount && possibleTypes.Count > 0)
                {
                    var usedStats = new List<StatType>();
                    //go through all possible types
                    for (int i = 0; i < possibleTypes.Count; i++)
                    {
                        //check if stat is rolled (over threshold)
                        if (Random.Range(0f, 1f) > 0.75f)
                        {
                            //roll value for stat
                            var statRuleSet = statRuleSets[(possibleTypes[i], eq.rarity)];
                            var rollValue = Random.Range(statRuleSet.minRollValue, statRuleSet.maxRollValue);
                            //round to 1 decimal place
                            rollValue *= 1000f;
                            rollValue = Mathf.Round(rollValue);
                            rollValue /= 1000f;

                            stats.Add(new EquipmentStat(possibleTypes[i],
                                new Modifier(statRuleSet.type, rollValue, eq, false)));
                            usedStats.Add(possibleTypes[i]);
                            if (stats.Count == statCount)
                            {
                                //we got enough stats, stop looping
                                break;
                            }
                        }
                    }

                    usedStats.ForEach(s => possibleTypes.Remove(s));
                }
            }

            eq.stats = stats;

            return eq;
        }

        private static List<StatType> GetPossibleStatTypes(Rarity rarity)
        {
            var types = new List<StatType>();
            foreach (StatType statType in (StatType[]) Enum.GetValues(typeof(StatType)))
            {
                if (statRuleSets.TryGetValue((statType, rarity), out var ruleSet) && ruleSet != null)
                {
                    types.Add(statType);
                }
            }

            return types;
        }
    }

    public class StatRuleSet
    {
        public ModifierType type;
        public float minRollValue;
        public float maxRollValue;

        public StatRuleSet(ModifierType type, float minRollValue, float maxRollValue)
        {
            this.type = type;
            this.minRollValue = minRollValue;
            this.maxRollValue = maxRollValue;
        }
    }

    public class SlotRuleSet
    {
        public int minStatCount;
        public int maxStatCount;

        public SlotRuleSet(int minStatCount, int maxStatCount)
        {
            this.minStatCount = minStatCount;
            this.maxStatCount = maxStatCount;
        }
    }
}