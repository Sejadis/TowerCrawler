using System;
using System.Collections.Generic;
using SejDev.Systems.Abilities;

namespace SejDev.Save
{
    [Serializable]
    public class UpgradeStateSave : Systems.Save.Save
    {
        public readonly List<UpgradeSaveState> states;

        public UpgradeStateSave(Dictionary<string, UpgradeState> states)
        {
            this.states = new List<UpgradeSaveState>();
            foreach (var key in states.Keys)
            {
                this.states.Add(new UpgradeSaveState(key, states[key]));
            }
        }

        public UpgradeStateSave(List<UpgradeSaveState> states)
        {
            this.states = states;
        }
    }
}