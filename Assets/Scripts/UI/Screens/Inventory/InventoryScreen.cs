using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.UI;
using UnityEngine;

namespace SejDev.UI.Screens.Inventory
{
    public class InventoryScreen : UIScreen
    {
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private GameObject itemElementPrefab;
        [SerializeField] private GameObject itemParent;
        [SerializeField] private GameObject tooltip;
        private Systems.Gear.Inventory inventory;
        private List<ItemElement> itemSlots = new List<ItemElement>();

        private void Start()
        {
            inventory = playerInventory.Inventory;
            CreateItemElements();
            UpdateInventory();
        }

        private void CreateItemElements()
        {
            for (int i = 0; i < inventory.MaxSpace; i++)
            {
                var go = Instantiate(itemElementPrefab, itemParent.transform);
                var element = go.GetComponent<ItemElement>();
                if (element != null)
                {
                    itemSlots.Add(element);
                    // element.OnElementClicked += OnAbilityElementClicked;
                    // element.OnElementEnter += OnAbilityElementEnter;
                    // element.OnElementExit += OnAbilityElementExit;
                    // element.Bind(ability, dragParent);
                    // elements.Add(element);
                }
            }
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                itemSlots[i].Bind(inventory.Items[i], tooltip, transform);
            }
        }
    }
}