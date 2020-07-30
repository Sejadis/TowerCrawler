using SejDev.Editor;
using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class UpgradeElement : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField, Rename] public AbilityUpgrade Upgrade { get; private set; }
        [SerializeField] private Image borderImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject inObj;
        [SerializeField] private GameObject outObj;

        public Transform IN => inObj.transform;
        public Transform OUT => outObj.transform;
        private bool isBound;
        public UpgradeScreen UpgradeScreen { get; private set; }

        private void Start()
        {
            UpdateState();
            iconImage.sprite = Upgrade.Icon;
        }

        public void Bind(AbilityUpgrade upgrade, UpgradeScreen screen)
        {
            if (isBound) return;
            this.Upgrade = upgrade;
            this.UpgradeScreen = screen;
            isBound = true;
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