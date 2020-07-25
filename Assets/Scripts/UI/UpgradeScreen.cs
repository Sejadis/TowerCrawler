using System;
using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Abilities;
using SejDev.Systems.UI;
using UnityEngine;

namespace SejDev.UI
{
    public class UpgradeScreen : UIScreen
    {
        [SerializeField] private UpgradeTree upgradeTree;
        [SerializeField] private List<UpgradeHolder> upgradeHolders = new List<UpgradeHolder>();

        private void Start()
        {
            foreach (var holder in upgradeHolders)
            {
                holder.UpgradeScreen = this;
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