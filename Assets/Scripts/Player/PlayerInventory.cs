using System.Collections.Generic;
using System.Linq;
using SejDev.Save;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        //TODO make private and serialize and grab automatically where possible
        public StatsManager statsManager;
        public Weapon weapon;
        public EquipmentHolder equipmentHolder;
        public WeaponHandler weaponHandler;
        public List<Item> startItems = new List<Item>();
        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            StatRollManager.Initialize();
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
            equipmentHolder = new EquipmentHolder(statsManager, Inventory, weaponHandler);
        }
    }
}