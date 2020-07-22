using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SejDev.Save;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewUpgradeTree",
        menuName = "Systems/Ability/Upgrade Tree")]
    public class UpgradeTree : ScriptableObject
    {
        public List<UpgradeRelation> upgrades = new List<UpgradeRelation>();

        [Serializable]
        public class UpgradeRelation
        {
            public AbilityUpgrade upgrade;
            public List<AbilityUpgrade> requiredUpgrades = new List<AbilityUpgrade>();
            public int requiredPointsSpent = 0;
        }

        public List<AbilityUpgrade> GetActiveUpgrades()
        {
            var upgradeIDs = upgrades.Select(relation => relation.upgrade.GUID).ToList();
            var activeUpgradeIDs = UpgradeManager.GetActiveUpgrades(upgradeIDs) ??
                                   throw new ArgumentNullException("UpgradeManager.GetActiveUpgrades(upgradeIDs)");
            return upgrades.Select(relation => relation.upgrade) //grab all updates from relation list
                .Where(upgrade => activeUpgradeIDs.Contains(upgrade.GUID)).ToList(); // filter only active ones
        }
    }
}