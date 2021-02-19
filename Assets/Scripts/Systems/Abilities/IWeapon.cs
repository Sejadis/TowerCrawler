using SejDev.Systems.Gear;

namespace SejDev.Systems.Abilities
{
    public interface IWeapon
    {
        WeaponController EquipWeapon(Weapon weapon);
    }
}