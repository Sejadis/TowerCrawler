using System;
using System.Collections.Generic;
using System.Linq;
using SejDev.Save;
using SejDev.Systems.Save;
using UnityEngine;


namespace SejDev.Systems.Abilities
{
    public static class AbilityManager
    {
        public static Dictionary<string, bool> unlockStates;

        public static void LoadAbilityStates()
        {
            // AbilityStateSave save = SaveSystem.Load<AbilityStateSave>("states", SaveType.sav);
            var save = SaveManager.GetSave<AbilityStateSave>();
            var dict = save?.states.ToDictionary(s => s.id, s => s.isUnlocked);
            unlockStates = dict ?? new Dictionary<string, bool>();
        }

        public static void SaveAbilityStates()
        {
            AbilityStateSave save = new AbilityStateSave(unlockStates);
            SaveManager.SetSave(save);
            // SaveSystem.Save(save, "states", SaveType.sav);
        }
    }
}