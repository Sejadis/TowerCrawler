using System;
using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.Core;
using SejDev.Systems.Gear;
using SejDev.Systems.UI;
using UnityEngine;

namespace SejDev.UI.Screens.Inventory
{
    public class InventoryScreen : UIScreen
    {
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private GameObject itemElementPrefab;
        [SerializeField] private GameObject itemParent;
        [SerializeField] private ObjectDescriber tooltip;
        [SerializeField] private List<GearElement> gearElements = new List<GearElement>();

        private Systems.Gear.Inventory inventory;
        private EquipmentHolder equipmentHolder;
        private readonly List<UIElement> itemSlots = new List<UIElement>();

        private void Start()
        {
            equipmentHolder = playerInventory.equipmentHolder;
            inventory = playerInventory.Inventory;

            inventory.OnInventoryChanged += OnInventoryChanged;

            foreach (var element in gearElements)
            {
                element.OnEquipmentDropped += EquipmentDropped;
            }

            CreateItemElements();
            UpdateInventory();
            UpdateEquipment();
        }

        private void UpdateEquipment()
        {
            foreach (var element in gearElements)
            {
                element.Equipment = equipmentHolder.GetItemForSlot(element.SlotType);
            }
        }

        private void EquipmentDropped(object sender, Equipment e)
        {
            equipmentHolder.EquipItem(e);
            (sender as GearElement).Equipment = e;
        }

        private void OnInventoryChanged()
        {
            UpdateInventory();
        }

        private void CreateItemElements()
        {
            for (int i = 0; i < inventory.MaxSpace; i++)
            {
                var go = Instantiate(itemElementPrefab, itemParent.transform);
                var element = go.GetComponent<UIElement>();
                if (element != null)
                {
                    itemSlots.Add(element);
                    element.OnElementEnter += OnItemElementEnter;
                    element.OnElementExit += OnItemElementExit;
                }
                else
                {
                    throw new Exception("ItemElement component doesnt exist on prefab");
                }
            }
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < inventory.MaxSpace; i++)
            {
                //bind slot to item if still items left to bind, clear slot otherwise
                itemSlots[i].Bind(i < inventory.Items.Count ? inventory.Items[i] : null, transform);
            }
        }

        private void OnItemElementExit(object sender, IDescribable e)
        {
            tooltip.gameObject.SetActive(false);
            tooltip.Reset();
        }

        private void OnItemElementEnter(object sender, IDescribable e)
        {
            if (e == null) return;
            tooltip.Fill(e);
            tooltip.gameObject.SetActive(true);
        }
    }
}