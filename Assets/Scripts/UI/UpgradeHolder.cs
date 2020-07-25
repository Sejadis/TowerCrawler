using System;
using SejDev.Editor;
using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class UpgradeHolder : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField, Rename] public AbilityUpgrade Upgrade { get; private set; }
        [SerializeField] private Image borderImage;
        public UpgradeScreen UpgradeScreen { get; set; }

        private void Start()
        {
            UpdateState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UpgradeScreen.ChangeState(Upgrade.GUID);
            UpdateState();
        }

        public void UpdateState()
        {
            var state = UpgradeManager.GetActiveState(Upgrade.GUID);
            borderImage.color = state ? Color.green : Color.red;
        }
    }
}