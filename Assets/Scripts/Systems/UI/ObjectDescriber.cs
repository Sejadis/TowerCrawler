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

        public IDescribable FallBackDescribable { get; set; }

        public void Fill(IDescribable describable)
        {
            if (describable == null) return;
            iconImage.sprite = describable.Icon;
            nameText.text = describable.Name;
            descriptionText.text = describable.Description;
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