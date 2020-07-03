using System.Collections.Generic;
using SejDev.Systems.StatusEffects;
using SejDev.UI;
using UnityEngine;

namespace SejDev.Player
{
    public class PlayerStatusEffectsUI : MonoBehaviour
    {
        [SerializeField]
        private StatusEffectManager statusEffectManager;
        [SerializeField]
        private GameObject layoutParent;
        [SerializeField]
        private GameObject statusEffectHolderTemplate;
        Dictionary<StatusEffect, GameObject> currentStatusEffects = new Dictionary<StatusEffect, GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            statusEffectManager.OnStatusEffectAdded += OnStatusEffectAdded;
            statusEffectManager.OnStatusEffectRemoved += OnStatusEffectRemoved;
            statusEffectHolderTemplate.SetActive(false);
        }

        private void OnStatusEffectAdded(object sender, StatusEffectChangedEventArgs e)
        {
            StatusEffectHolderUI statusEffectHolder = Instantiate(statusEffectHolderTemplate, layoutParent.transform).GetComponent<StatusEffectHolderUI>();
            statusEffectHolder.Bind(e.statusEffect);
            currentStatusEffects.Add(e.statusEffect, statusEffectHolder.gameObject);
            statusEffectHolder.gameObject.SetActive(true);
        }

        private void OnStatusEffectRemoved(object sender, StatusEffectChangedEventArgs e)
        {
            if (currentStatusEffects.ContainsKey(e.statusEffect))
            {
                Destroy(currentStatusEffects[e.statusEffect]);
                currentStatusEffects.Remove(e.statusEffect);
            }
        }
    }
}
