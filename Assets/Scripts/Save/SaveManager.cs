using System;
using System.Collections.Generic;
using SejDev.Systems.Save;
using UnityEngine;

namespace SejDev.Save
{
    public static class SaveManager
    {
        // private static  UpgradeStateSave upgradeState;
        // private static AbilityStateSave abilityState;

        private static readonly string saveFileName = "save";
        private static bool isLoaded;

        private static readonly Dictionary<Type, Systems.Save.Save> trackedSaves =
            new Dictionary<Type, Systems.Save.Save>()
            {
                {typeof(UpgradeStateSave), null},
                {typeof(AbilityStateSave), null},
            };

        public static void SetSave<T>(T save, bool skipWritingToFile = false) where T : Systems.Save.Save
        {
            if (trackedSaves.ContainsKey(typeof(T)))
            {
                trackedSaves[typeof(T)] = save;
                if (!skipWritingToFile)
                {
                    SaveToFile();
                }
            }
            else
            {
                Debug.LogWarning($"Trying to set untracked save: {typeof(T)}");
            }
        }

        public static void SaveToFile()
        {
            SaveSystem.Save(new FullSave(trackedSaves), saveFileName, SaveType.sav);
        }

        public static void LoadSave()
        {
            var save = SaveSystem.Load<FullSave>(saveFileName, SaveType.sav);
            if (save != null)
            {
                trackedSaves.Clear();
                trackedSaves[typeof(UpgradeStateSave)] = save.upgradeState;
                trackedSaves[typeof(AbilityStateSave)] = save.abilityState;
            }
        }

        public static T GetSave<T>() where T : Systems.Save.Save
        {
            if (!isLoaded)
            {
                LoadSave();
            }

            trackedSaves.TryGetValue(typeof(T), out var save);
            return save as T;
        }
    }
}