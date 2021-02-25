using System;
using System.Collections.Generic;
using SejDev.Editor;
using SejDev.Save;
using SejDev.Systems.Core;
using SejDev.Systems.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SejDev.Systems.Equipment
{
    [CreateAssetMenu(fileName = "Assets/Resources/Items/Gear/NewArmor",
        menuName = "Systems/Equipment/Armor")]
    public class Equipment : Item
    {
        [field: Rename, SerializeField] public Rarity rarity { get; set; } //TODO hide in custom inspector
        [field: Rename, SerializeField] public EquipSlotType EquipSlot { get; protected set; }

        public List<GuaranteedStatData> GuaranteedStats = new List<GuaranteedStatData>();
        public List<EquipmentStat> stats = new List<EquipmentStat>();


        public Equipment CreateDeepClone()
        {
            var clone = Instantiate(this);
            clone.guid = guid;
            clone.id = id;
            return clone;
        }

        public void RollStats()
        {
            stats.Clear();
            var threshold = 0.5f;
            var types = (StatType[]) Enum.GetValues(typeof(StatType));
            rarity = (Rarity) Random.Range(0, types.Length);
            foreach (StatType statType in types)
            {
                if (statType == StatType.None) continue;
                var rnd = Random.Range(0f, 1f);
                if (rnd >= threshold)
                {
                    var mod = new Modifier(ModifierType.Absolute, Random.Range(1, 11), this, false);
                    var stat = new EquipmentStat(statType, mod);
                    stats.Add(stat);
                }
            }
        }

        public Equipment SetValuesFromSave(EquipmentStateSave save)
        {
            save.stats.ForEach(eqStat => eqStat.modifier.source = this);
            stats = save.stats;
            rarity = save.rarity;
            return this;
        }
    }
}