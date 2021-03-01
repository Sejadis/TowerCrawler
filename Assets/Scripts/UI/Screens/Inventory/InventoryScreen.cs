using System;
using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;
using SejDev.Systems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SejDev.UI.Screens.Inventory
{
    public class InventoryScreen : UIScreen
    {
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private StatsManager statsManager;
        [SerializeField] private GameObject itemElementPrefab;
        [SerializeField] private GameObject itemParent;

        [SerializeField] private GameObject statParent;
        [SerializeField] private GameObject statPrefab;

        [SerializeField] private ObjectDescriber tooltip;
        [SerializeField] private List<EquipmentElement> equipmentElements = new List<EquipmentElement>();
        [SerializeField] private List<CurrencyElement> currencyElements = new List<CurrencyElement>();
        private Systems.Equipment.Inventory inventory;
        private EquipmentHolder equipmentHolder;
        private readonly List<UIElement> itemSlots = new List<UIElement>();

        private void Start()
        {
            statsManager.OnAnyStatChanged += OnStatChanged;
            equipmentHolder = playerInventory.equipmentHolder;
            inventory = playerInventory.Inventory;

            inventory.OnInventoryChanged += OnInventoryChanged;

            foreach (var element in equipmentElements)
            {
                element.OnElementDropped += EquipmentDropped;
                element.OnElementEnter += OnItemElementEnter;
                element.OnElementExit += OnItemElementExit;
            }

            foreach (var element in currencyElements)
            {
                element.OnElementEnter += OnItemElementEnter;
                element.OnElementExit += OnItemElementExit;
            }

            BindEquipment();
            CreateItemElements();
            UpdateInventory();
            UpdateEquipment();
            OnStatChanged(null, null);
        }

        private void OnStatChanged(object sender, EventArgs e)
        {
            foreach (Transform child in statParent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var stat in statsManager.stats)
            {
                var go = Instantiate(statPrefab, statParent.transform, false);
                var text = go.GetComponent<TextMeshProUGUI>();
                text.text = stat.ToString();
                go.SetActive(true);
            }
        }

        private void UpdateEquipment()
        {
            foreach (var element in equipmentElements)
            {
                element.Equipment = equipmentHolder.GetItemForSlot(element.SlotType);
            }
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
                    element.OnElementDropped += OnItemDropped;
                }
                else
                {
                    throw new Exception("ItemElement component doesnt exist on prefab");
                }
            }
        }

        private void EquipmentDropped(object sender, (IBeginDragHandler, IDescribable) payload)
        {
            if (payload.Item2 is Equipment eq)
            {
                equipmentHolder.EquipItem(eq);
            }
        }

        private void OnItemDropped(object sender, (IBeginDragHandler, IDescribable) payload)
        {
            if (payload.Item1 is EquipmentElement eqElement && payload.Item2 is Equipment eq)
            {
                equipmentHolder.UnEquipItem(eq);
                eqElement.Equipment = null;
            }
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < inventory.MaxSpace; i++)
            {
                //bind slot to item if still items left to bind, clear slot otherwise
                itemSlots[i].Bind(i < inventory.Items.Count ? inventory.Items[i] : null, transform);
            }

            foreach (var currency in inventory.Currencies)
            {
                var element =
                    currencyElements.Find(curElement => curElement.CurrencyData.Equals(currency.CurrencyData));
                element.Amount = currency.Amount;
            }
        }

        private void BindEquipment()
        {
            equipmentElements.ForEach(eqElements => eqElements.Bind(transform));
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