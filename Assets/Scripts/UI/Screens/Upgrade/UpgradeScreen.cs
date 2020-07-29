using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Abilities;
using SejDev.Systems.UI;
using UnityEngine;

namespace SejDev.UI
{
    public class UpgradeScreen : UIScreen
    {
        [SerializeField] private GameObject upgradeHolderPrefab;
        [SerializeField] private GameObject vertParent;
        [SerializeField] private GameObject groupPrefab;

        private UpgradeTree upgradeTree;
        private readonly List<UpgradeHolder> upgradeHolders = new List<UpgradeHolder>();
        private readonly Dictionary<int, GameObject> conditionGroups = new Dictionary<int, GameObject>();

        public void CreateFromUpgradeTree(UpgradeTree upgradeTree)
        {
            CleanUpTree();
            this.upgradeTree = upgradeTree;
            if (upgradeTree == null) return;

            foreach (var upgradeRelation in upgradeTree.upgrades)
            {
                var holderObj = Instantiate(upgradeHolderPrefab, transform);
                var holder = holderObj.GetComponent<UpgradeHolder>();

                holder?.Bind(upgradeRelation.upgrade, this);
                upgradeHolders.Add(holder);

                if (!conditionGroups.ContainsKey(upgradeRelation.requiredPointsSpent))
                {
                    conditionGroups[upgradeRelation.requiredPointsSpent] =
                        Instantiate(groupPrefab, vertParent.transform);
                }

                holderObj.transform.parent = conditionGroups[upgradeRelation.requiredPointsSpent].transform;
            }

            // foreach (var upgradeRelation in upgradeTree.upgrades)
            // {
            //     var obj = upgradeHolders.First(holder => holder.Upgrade.Equals(upgradeRelation.upgrade)).gameObject;
            //     foreach (var requiredUpgrade in upgradeRelation.requiredUpgrades)
            //     {
            //         var reqObj = upgradeHolders.First(holder => holder.Upgrade.Equals(requiredUpgrade)).gameObject;
            //         var lineRenderer = obj.AddComponent<LineRenderer>();
            //         lineRenderer.SetPositions(new[] {obj.transform.position, reqObj.transform.position});
            //     }
            // }
        }

        private void CleanUpTree()
        {
            upgradeHolders.Clear();
            foreach (var key in conditionGroups.Keys)
            {
                foreach (Transform child in conditionGroups[key].transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public bool ChangeState(string id)
        {
            var state = upgradeTree.ChangeState(id, out var implicitChanges);
            foreach (var implicitChange in implicitChanges)
            {
                upgradeHolders.First(holder => holder.Upgrade.GUID.Equals(implicitChange)).UpdateState();
            }

            return state;
        }
    }
}