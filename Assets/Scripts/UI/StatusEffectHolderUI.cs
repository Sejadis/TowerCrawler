using SejDev.Systems.StatusEffects;
using TMPro;
using UnityEngine;

namespace SejDev.UI
{
    public class StatusEffectHolderUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI cooldownText;
        [SerializeField] private TextMeshProUGUI stackText;
        private bool hasStacks;
        private EffectType effectType;

        // Start is called before the first frame update
        public void Bind(StatusEffect statusEffect)
        {
            effectType = statusEffect.EffectType;
            nameText.text = statusEffect.Name;
            if (effectType.Equals(EffectType.Duration))
            {
                cooldownText.text = statusEffect.Duration.ToString("F1");
            }
            else
            {
                cooldownText.text = "✔";
            }

            statusEffect.OnStatusEffectChanged += OnStatusEffectChanged;

            if (statusEffect is StackableStatusEffect)
            {
                hasStacks = true;
                stackText.text = (statusEffect as StackableStatusEffect).CurrentStacks.ToString();
            }
            else
            {
                stackText.enabled = false;
            }
        }

        private void OnStatusEffectChanged(object sender, StatusEffectChangedEventArgs e)
        {
            if (effectType.Equals(EffectType.Duration))
            {
                cooldownText.text = e.durationLeft.ToString("F1");
            }
            
            if (hasStacks && e.currentStacks != null)
            {
                stackText.text = e.currentStacks.ToString();
            }
        }
    }
}