using System;
using SejDev.Systems.Gear;

namespace SejDev.Save
{
    [Serializable]
    public class EquipmentSave : Systems.Save.Save
    {
        public string weaponID;

        public EquipmentSave(Weapon weapon)
        {
            weaponID = weapon?.GUID ?? string.Empty;
        }
    }
}