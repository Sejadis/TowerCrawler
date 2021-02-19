using System;
using System.Collections.Generic;

namespace SejDev.Save
{
    [Serializable]
    public class FullSave : Systems.Save.Save
    {
        public AbilityStateSave abilityState;
        public UpgradeStateSave upgradeState;
        public EquippedAbilitySave equippedAbilities;
        public InventorySave inventory;
        public EquipmentSave equipment;

        public FullSave(Dictionary<Type, Systems.Save.Save> saves)
        {
            abilityState = saves[typeof(AbilityStateSave)] as AbilityStateSave;
            upgradeState = saves[typeof(UpgradeStateSave)] as UpgradeStateSave;
            equippedAbilities = saves[typeof(EquippedAbilitySave)] as EquippedAbilitySave;
            equipment = saves[typeof(EquipmentSave)] as EquipmentSave;
        }
    }
}