using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Systems.Equipment
{
    public class WeaponHandler : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform weaponOrigin;
        [SerializeField] private AbilityHandler abilityHandler;

        private WeaponController currentWeaponControllerInstance;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public WeaponController EquipWeapon(Weapon weapon)
        {
            if (currentWeaponControllerInstance != null)
            {
                Destroy(currentWeaponControllerInstance.gameObject);
            }

            GameObject weaponInstance = Instantiate(weapon.prefab, weaponOrigin, false);
            currentWeaponControllerInstance = weaponInstance.GetComponent<WeaponController>();

            abilityHandler.ChangeAbility(weapon.baseAbility, AbilitySlot.WeaponBase, currentWeaponControllerInstance);
            abilityHandler.ChangeAbility(weapon.specialAbility, AbilitySlot.WeaponSpecial,
                currentWeaponControllerInstance);

            return currentWeaponControllerInstance;
        }

        public void UnEquipWeapon()
        {
            abilityHandler.ChangeAbility(null, AbilitySlot.WeaponBase);
            abilityHandler.ChangeAbility(null, AbilitySlot.WeaponSpecial,
                currentWeaponControllerInstance);
            if (currentWeaponControllerInstance != null)
            {
                Destroy(currentWeaponControllerInstance.gameObject);
            }
        }
    }
}