using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.Core;
using SejDev.Systems.Crafting;
using SejDev.Systems.Equipment;
using SejDev.Systems.UI;
using SejDev.UI.Screens.Inventory;
using UnityEngine;

namespace SejDev.UI.Screens.Crafting
{
    public class CraftingScreen : UIScreen
    {
        [SerializeField] private Transform blueprintParent;
        [SerializeField] private GameObject blueprintPrefab;
        [SerializeField] private ObjectDescriber tooltip;
        [SerializeField] private List<CurrencyElement> currencyElements = new List<CurrencyElement>();

        private List<CraftingBlueprint> blueprints = new List<CraftingBlueprint>();
        private CraftingBlueprint selectedBlueprint;
        private CraftingHandler craftingHandler;
        private Systems.Equipment.Inventory inventory;

        private void Start()
        {
            blueprints = ResourceManager.Instance.blueprintList;
            //TODO refactor to get references a better way
            craftingHandler = GameManager.Instance.player.GetComponent<CraftingHandler>();
            inventory = GameManager.Instance.player.GetComponent<PlayerInventory>().Inventory;
            inventory.OnInventoryChanged += SetCurrencies;
            PopulateBlueprints();
            SetCurrencies();
        }

        private void PopulateBlueprints()
        {
            foreach (var blueprint in blueprints)
            {
                var go = Instantiate(blueprintPrefab, blueprintParent, false);
                var element = go.GetComponent<UIElement>();
                element.Bind(blueprint);
                element.OnElementEnter += OnElementEnter;
                element.OnElementExit += OnElementExit;
                element.OnElementClicked += OnElementClicked;
            }
        }

        private void OnElementClicked(object sender, IDescribable e)
        {
            tooltip.FallBackDescribable = e;
            selectedBlueprint = e as CraftingBlueprint;
        }

        private void OnElementExit(object sender, IDescribable e)
        {
            tooltip.Reset();
            SetCurrencyColors();
        }

        private void OnElementEnter(object sender, IDescribable e)
        {
            if (e == null) return;
            tooltip.Fill(e);
            SetCurrencyColors(e as CraftingBlueprint);
        }

        public void TryCraftSelectedItem()
        {
            craftingHandler.TryCraft(selectedBlueprint, out var item);
            if (item)
            {
                inventory.AddItem(item as Equipment);
            }
        }

        private void SetCurrencyColors(CraftingBlueprint blueprint = null)
        {
            if (blueprint == null)
            {
                blueprint = selectedBlueprint;
            }

            if (blueprint == null)
            {
                foreach (var element in currencyElements)
                {
                    element.Color = Color.white;
                }
            }
            else
            {
                foreach (var element in currencyElements)
                {
                    //that element is in used in the selected blueprint
                    var craftingCost =
                        blueprint.CraftingCosts.Find(cost => cost.currencyData.Equals(element.CurrencyData));
                    if (craftingCost != null)
                    {
                        element.Color = inventory.ContainsCurrency(craftingCost.currencyData, craftingCost.amount)
                            ? Color.green
                            : Color.red;
                    }
                    else
                    {
                        element.Color = Color.white;
                    }
                }
            }
        }

        private void SetCurrencies()
        {
            foreach (var currency in inventory.Currencies)
            {
                var element =
                    currencyElements.Find(curElement => curElement.CurrencyData.Equals(currency.CurrencyData));
                element.Amount = currency.Amount;
            }
        }
    }
}