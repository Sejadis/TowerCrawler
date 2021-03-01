using System.Collections.Generic;
using SejDev.Editor;
using SejDev.Save;
using SejDev.Systems.Core;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.Systems.Equipment
{
    [CreateAssetMenu(fileName = "Assets/Resources/Items/Gear/NewArmor",
        menuName = "Systems/Equipment/Armor")]
    public class Equipment : Item
    {
        public Rarity rarity { get; set; }
        [field: Rename, SerializeField] public EquipSlotType EquipSlot { get; protected set; }

        public List<GuaranteedStatData> GuaranteedStats = new List<GuaranteedStatData>();
        [HideInInspector] public List<EquipmentStat> stats = new List<EquipmentStat>();


        public Equipment CreateDeepClone()
        {
            var clone = Instantiate(this);
            clone.guid = guid;
            clone.id = id;
            return clone;
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