using System.Collections.Generic;
using System.Linq;
using SejDev.Save;
using SejDev.Systems.Save;

namespace SejDev.Systems.Abilities
{
    public static class UpgradeManager
    {
        static Dictionary<string, UpgradeState> states;
        private static bool isLoaded;

        public static List<string> GetActiveUpgrades(List<string> upgradeIDs)
        {
            foreach (var id in upgradeIDs)
            {
                GetActiveState(id);
            }

            return states.Where(
                    kvp => upgradeIDs.Contains(kvp.Key) &&
                           kvp.Value.isActive //check ids from parameter for active state
                )
                .Select(kvp => kvp.Key) // take only key from resulting KVP enumerator
                .ToList();
        }

        public static List<string> GetInActiveUpgrades(List<string> upgradeIDs)
        {
            foreach (var id in upgradeIDs)
            {
                GetActiveState(id);
            }

            return states.Where(
                    kvp => upgradeIDs.Contains(kvp.Key) &&
                           !kvp.Value.isActive //check ids from parameter for active state
                )
                .Select(kvp => kvp.Key) // take only key from resulting KVP enumerator
                .ToList();
        }

        public static void Load()
        {
            // UpgradeStateSave save = SaveSystem.Load<UpgradeStateSave>("upgradeStates", SaveType.sav);
            var save = SaveManager.GetSave<UpgradeStateSave>();
            var dict = save?.states.ToDictionary(s => s.id, s => s.state);
            states = dict ?? new Dictionary<string, UpgradeState>();
            isLoaded = true;
        }

        public static void Save()
        {
            UpgradeStateSave save = new UpgradeStateSave(states);
            SaveManager.SetSave(save);
            // SaveSystem.Save(save, "upgradeStates", SaveType.sav);
        }

        public static bool GetActiveState(string id)
        {
            if (!isLoaded)
            {
                Load();
            }

            if (!states.ContainsKey(id))
            {
                states[id] = new UpgradeState();
            }

            return states[id].isActive;
        }

        public static bool SetActive(string upgradeGuid, bool isActive)
        {
            if (!isLoaded)
            {
                Load();
            }

            states[upgradeGuid].isActive = isActive;
            Save();
            return isActive;
        }
    }
}