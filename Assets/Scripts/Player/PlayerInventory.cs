using System.Collections.Generic;
using SejDev.Save;
using SejDev.Systems.Gear;
using UnityEngine;

namespace SejDev.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public Weapon weapon;
        public EquipmentHolder equipmentHolder;
        public WeaponHandler weaponHandler;
        public List<Item> startItems = new List<Item>();
        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory(40, startItems);
            var save = new EquipmentSave(weapon);
            SaveManager.SetSave(save);
        }

        private void Start()
        {
            var equipmentSave = SaveManager.GetSave<EquipmentSave>();
            weapon = ResourceManager.Instance.GetEquipmentByID(equipmentSave.weaponID) as Weapon;
            equipmentHolder = new EquipmentHolder(Inventory, weapon);
            weaponHandler.EquipWeapon(weapon);
        }
    }
}