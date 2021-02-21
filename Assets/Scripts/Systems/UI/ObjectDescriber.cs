﻿using SejDev.Systems.Core;
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

        public IDescribable FallBackDescribable { get; set; }

        public void Fill(IDescribable describable)
        {
            if (describable == null) return;
            iconImage.sprite = describable.Icon;
            nameText.text = describable.Name;
            descriptionText.text = describable.Description;
            if (describable is Equipment.Equipment eq)
            {
                foreach (Transform child in statParent.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (var stat in eq.stats)
                {
                    var go = new GameObject("Text", typeof(TextMeshProUGUI));
                    go.transform.parent = statParent.transform;
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;
                    go.transform.rotation = new Quaternion(0, 0, 0, 0);
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