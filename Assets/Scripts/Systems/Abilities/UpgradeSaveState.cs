using System;

namespace SejDev.Systems.Abilities
{
    [Serializable]
    public class UpgradeSaveState
    {
        public string id;
        public UpgradeState state;

        public UpgradeSaveState(string id, UpgradeState upgradeState)
        {
            this.id = id;
            state = upgradeState;
        }
    }
}