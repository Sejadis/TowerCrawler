using System;
using SejDev.Systems.Core;
using SejDev.Systems.Crafting;
using SejDev.UI.Screens.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SejDev.Systems.UI
{
    public class ObjectDescriber : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        [FormerlySerializedAs("statParent")] [SerializeField]
        private GameObject contentParent;

        [SerializeField] private Image backgroundImage;
        [SerializeField] private GameObject prefab;
        public IDescribable FallBackDescribable { get; set; }

        public void Fill(IDescribable describable)
        {
            if (describable == null) return;
            iconImage.sprite = describable.Icon;
            nameText.text = describable.Name;
            descriptionText.text = describable.Description;
            switch (describable)
            {
                case Equipment.Equipment eq:
                    FillEquipment(eq);
                    break;
                case CraftingBlueprint bp:
                    FillBlueprint(bp);
                    break;
            }
        }

        private void FillBlueprint(CraftingBlueprint bp)
        {
            backgroundImage.color = Utility.RarityColors[bp.CraftingResult.rarity];
            foreach (Transform child in contentParent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var cost in bp.CraftingCosts)
            {
                var go = Instantiate(prefab, contentParent.transform, false);
                var element = go.GetComponent<CurrencyElement>();
                if (element)
                {
                    element.CurrencyData = cost.currencyData;
                    element.Amount = cost.amount;
                }
            }
        }

        private void FillEquipment(Equipment.Equipment eq)
        {
            backgroundImage.color = Utility.RarityColors[eq.rarity];
            nameText.text += " " + Enum.GetName(typeof(Rarity), eq.rarity);
            foreach (Transform child in contentParent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var stat in eq.stats)
            {
                var go = new GameObject("Text", typeof(TextMeshProUGUI));
                go.transform.SetParent(contentParent.transform, false);
                var text = go.GetComponent<TextMeshProUGUI>();
                text.enableAutoSizing = true;
                text.text = stat.ToString();
            }
        }

        public void Reset(bool ignoreFallback = false)
        {
            if (!ignoreFallback && FallBackDescribable != null)
            {
                Fill(FallBackDescribable);
            }
            else
            {
                iconImage.sprite = null;
                nameText.text = string.Empty;
                descriptionText.text = string.Empty;
            }
        }
    }
}