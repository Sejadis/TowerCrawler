using System;
using System.Collections.Generic;
using System.Linq;
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

        private void OnValidate()
        {
            foreach (var upgradeRelation in upgrades)
            {
                if (upgradeRelation == null) return;
                var min = GetMinPointsSpend(upgradeRelation.upgrade);
                upgradeRelation.requiredPointsSpent = Mathf.Max(min, upgradeRelation.requiredPointsSpent);
            }
        }

        private int GetMinPointsSpend(AbilityUpgrade upgrade)
        {
            int i = 0;
            var relation = upgrades.FirstOrDefault(rel => rel.upgrade.Equals(upgrade));
            if (relation != null && relation.requiredUpgrades.Count > 0)
            {
                var x = new List<int>();
                foreach (var requiredUpgrade in relation.requiredUpgrades)
                {
                    x.Add(GetMinPointsSpend(requiredUpgrade));
                }

                i = x.Max() + 1;
            }

            return i;
        }

        public List<AbilityUpgrade> GetActiveUpgrades()
        {
            var upgradeIDs = upgrades.Select(relation => relation.upgrade.GUID)
                .ToList();
            // Debug.Log($"checking {upgradeIDs.Count} upgrades");
            var activeUpgradeIDs = UpgradeManager.GetActiveUpgrades(upgradeIDs) ??
                                   throw new ArgumentNullException("UpgradeManager.GetActiveUpgrades(upgradeIDs)");
            // Debug.Log($"got {activeUpgradeIDs.Count}");
            return upgrades.Select(relation => relation.upgrade) //grab all updates from relation list
                .Where(upgrade => activeUpgradeIDs.Contains(upgrade.GUID))
                .ToList(); // filter only active ones
        }

        public void ChangeState(string id, out List<string> implicitChanges)
        {
            implicitChanges = new List<string>();
            var newState = !UpgradeManager.GetActiveState(id);
            UpgradeManager.SetActive(id, newState);

            if (!newState)
            {
                //setting upgrade to inactive, deactivate everything that requires this
                var upgrade = upgrades.First(relation => relation.upgrade.GUID.Equals(id)).upgrade;

                var first = upgrades.Where(relation => relation.requiredUpgrades.Contains(upgrade));
                var upgradeIDs = first.Select(relation => relation.upgrade.GUID)
                    .ToList();
                var activeUpgradeIDs = UpgradeManager.GetActiveUpgrades(upgradeIDs);

                implicitChanges.AddRange(activeUpgradeIDs);
                foreach (var inActiveID in activeUpgradeIDs)
                {
                    ChangeState(inActiveID, out var recursiveImplicitChanges);
                    implicitChanges.AddRange(recursiveImplicitChanges);
                }
            }
            else
            {
                //setting upgrade to active, activating everything this upgrade requires

                //get all ids this upgrade requires
                var upgradeIDs = upgrades.First(relation => relation.upgrade.GUID.Equals(id))
                    .requiredUpgrades
                    .Select(upgrade => upgrade.GUID).ToList();

                //filter out to only the inactive ones
                var inActiveUpgradeIDs = UpgradeManager.GetInActiveUpgrades(upgradeIDs);
                implicitChanges.AddRange(inActiveUpgradeIDs);
                //change state for each of them
                foreach (var inActiveID in inActiveUpgradeIDs)
                {
                    ChangeState(inActiveID, out var recursiveImplicitChanges);
                    implicitChanges.AddRange(recursiveImplicitChanges);
                }
            }
        }
    }
}