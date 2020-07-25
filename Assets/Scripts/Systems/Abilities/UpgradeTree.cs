using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NSubstitute.Core;
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

        public bool ChangeState(string id, out List<string> implicitChanges)
        {
            implicitChanges = new List<string>();
            var state = UpgradeManager.GetActiveState(id);
            state = UpgradeManager.SetActive(id, !state);
            if (!state)
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

            return state;
        }
    }
}