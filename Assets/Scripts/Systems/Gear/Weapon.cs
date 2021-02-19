using SejDev.Editor;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Systems.Gear
{
    [CreateAssetMenu(fileName = "Assets/Resources/Items/Gear/NewWeapon",
        menuName = "Systems/Gear/Weapon")]
    public class Weapon : Equipment
    {
        [field: Rename]
        [field: SerializeField]
        public override EquipSlotType EquipSlot { get; protected set; }

        public WeaponAbility baseAbility;
        public WeaponAbility specialAbility;
        public GameObject prefab;
        public int damage;
    }
}