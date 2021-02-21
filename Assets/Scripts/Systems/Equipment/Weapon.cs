using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Systems.Equipment
{
    [CreateAssetMenu(fileName = "Assets/Resources/Items/Gear/NewWeapon",
        menuName = "Systems/Equipment/Weapon")]
    public class Weapon : Equipment
    {
        public WeaponAbility baseAbility;
        public WeaponAbility specialAbility;
        public GameObject prefab;
        public int damage;
    }
}