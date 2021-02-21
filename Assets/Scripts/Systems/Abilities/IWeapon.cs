using SejDev.Systems.Equipment;

namespace SejDev.Systems.Abilities
{
    public interface IWeapon
    {
        WeaponController EquipWeapon(Weapon weapon);
    }
}