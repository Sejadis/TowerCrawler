﻿using SejDev.Systems.Abilities;
using SejDev.Systems.Gear;
using UnityEngine;

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
        abilityHandler.ChangeAbility(weapon.specialAbility, AbilitySlot.WeaponSpecial, currentWeaponControllerInstance);

        return currentWeaponControllerInstance;
    }
}