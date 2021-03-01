using System;
using SejDev.Systems.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SejDev.Systems.UI
{
    public class ObjectDescriber : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private GameObject statParent;
        [SerializeField] private Image backgroundImage;

        public IDescribable FallBackDescribable { get; set; }

        public void Fill(IDescribable describable)
        {
            if (describable == null) return;
            iconImage.sprite = describable.Icon;
            nameText.text = describable.Name;
            descriptionText.text = describable.Description;
            if (describable is Equipment.Equipment eq)
            {
                backgroundImage.color = Utility.RarityColors[eq.rarity];
                nameText.text += " " + Enum.GetName(typeof(Rarity), eq.rarity);
                foreach (Transform child in statParent.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (var stat in eq.stats)
                {
                    var go = new GameObject("Text", typeof(TextMeshProUGUI));
                    go.transform.SetParent(statParent.transform, false);
                    var text = go.GetComponent<TextMeshProUGUI>();
                    text.enableAutoSizing = true;
                    text.text = stat.ToString();
                }
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