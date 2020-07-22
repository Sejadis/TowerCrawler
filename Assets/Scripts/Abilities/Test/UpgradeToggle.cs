using System;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities.Test
{
    public class UpgradeToggle : MonoBehaviour
    {
        public bool isActive;
        public AbilityUpgrade upgrade;

        private void Start()
        {
            isActive = UpgradeManager.GetActiveState(upgrade.GUID);
        }

        private void OnTriggerEnter(Collider other)
        {
            isActive = !isActive;
            UpgradeManager.SetActive(upgrade.GUID, isActive);
        }
    }
}