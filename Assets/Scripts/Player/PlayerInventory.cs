using System.Collections.Generic;
using System.Linq;
using SejDev.Save;
using SejDev.Systems.Equipment;
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
            var inventorySave = SaveManager.GetSave<InventorySave>();
            Inventory = new Inventory(40, inventorySave?.GetItems());
            Inventory.OnInventoryChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged()
        {
            var save = new InventorySave(Inventory.Items.ToList());
            SaveManager.SetSave(save);
        }

        private void Start()
        {
            equipmentHolder = new EquipmentHolder(Inventory, weaponHandler);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                var eq = (startItems[0] as Equipment).CreateDeepClone();
                eq.RollStats();
                Inventory.AddItem(eq);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                var eq = (startItems[1] as Equipment).CreateDeepClone();
                eq.RollStats();
                Inventory.AddItem(eq);
            }
        }
    }
}