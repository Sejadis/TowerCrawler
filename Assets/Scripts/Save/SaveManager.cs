using System;
using System.Collections.Generic;
using SejDev.Systems.Save;
using UnityEngine;

namespace SejDev.Save
{
    public static class SaveManager
    {
        private const string SaveFileName = "save";
        private static bool isLoaded;

        private static readonly Dictionary<Type, Systems.Save.Save> trackedSaves =
            new Dictionary<Type, Systems.Save.Save>()
            {
                {typeof(UpgradeStateSave), null},
                {typeof(AbilityStateSave), null},
                {typeof(EquippedAbilitySave), null},
                {typeof(EquipmentSave), null}
            };

        public static void SetSave<T>(T save, bool skipWritingToFile = false) where T : Systems.Save.Save
        {
            if (!isLoaded)
            {
                LoadSave();
            }

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
            SaveSystem.Save(new FullSave(trackedSaves), SaveFileName, SaveType.sav);
        }

        public static void LoadSave()
        {
            var save = SaveSystem.Load<FullSave>(SaveFileName, SaveType.sav);
            if (save != null)
            {
                trackedSaves.Clear();
                trackedSaves[typeof(UpgradeStateSave)] = save.upgradeState;
                trackedSaves[typeof(AbilityStateSave)] = save.abilityState;
                trackedSaves[typeof(EquippedAbilitySave)] = save.equippedAbilities;
                trackedSaves[typeof(EquipmentSave)] = save.equipment;
            }

            isLoaded = true;
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