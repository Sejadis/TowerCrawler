using System.Collections.Generic;
using System.Linq;
using SejDev.Save;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public static class AbilityManager
    {
        public static Dictionary<string, bool> unlockStates;
        private static Ability core1;
        private static Ability core2;
        private static Ability core3;
        private static Ability weaponBase;
        private static Ability weaponSpecial;
        private static bool isLoaded;

        public static Ability Core1
        {
            get
            {
                if (!isLoaded)
                {
                    Load();
                }

                return core1;
            }
        }

        public static Ability Core2
        {
            get
            {
                if (!isLoaded)
                {
                    Load();
                }

                return core2;
            }
        }

        public static Ability Core3
        {
            get
            {
                if (!isLoaded)
                {
                    Load();
                }

                return core3;
            }
        }

        public static Ability WeaponBase
        {
            get
            {
                if (!isLoaded)
                {
                    Load();
                }

                return weaponBase;
            }
        }

        public static Ability WeaponSpecial
        {
            get
            {
                if (!isLoaded)
                {
                    Load();
                }

                return weaponSpecial;
            }
        }

        private static void Load()
        {
            LoadAbilityStates();
            LoadEquippedAbilities();
            isLoaded = true;
        }

        private static void LoadEquippedAbilities()
        {
            var save = SaveManager.GetSave<EquippedAbilitySave>();
            if (save != null)
            {
                core1 = GetAbilityByID(save.core1ID);
                core2 = GetAbilityByID(save.core2ID);
                core3 = GetAbilityByID(save.core3ID);
            }
        }

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

        public static void SetAbility(Ability ability, AbilitySlot slot)
        {
            switch (slot)
            {
                case AbilitySlot.Core1:
                    core1 = ability;
                    break;
                case AbilitySlot.Core2:
                    core2 = ability;
                    break;
                case AbilitySlot.Core3:
                    core3 = ability;
                    break;
                // case AbilitySlot.WeaponBase:
                //     weaponBase = ability;
                //     break;
                // case AbilitySlot.WeaponSpecial:
                //     weaponSpecial = ability;
                //     break;
                default:
                    Debug.LogWarning("Setting ability for unknown slot");
                    break;
            }

            var save = new EquippedAbilitySave(core1, core2, core3);
            SaveManager.SetSave(save);
        }

        public static Ability GetAbilityByID(string id)
        {
            foreach (var abilityList in ResourceManager.Instance.abilityLists)
            {
                foreach (var ability in abilityList.abilities)
                {
                    if (ability.GUID.Equals(id))
                    {
                        return ability;
                    }
                }
            }

            return null;
        }
    }
}