using System;
using SejDev.Player;
using SejDev.Systems.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.UI
{
    public class UIManager : MonoBehaviour
    {
        public UIScreen upgradeScreen;
        public MouseLook player;

        private void Start()
        {
            InputManager.Instance.OnUpgradeUI += UpgradeUi;
        }

        private void UpgradeUi(InputAction.CallbackContext obj)
        {
            var state = upgradeScreen.IsActive;
            if (state)
            {
                upgradeScreen.Hide();
            }
            else
            {
                upgradeScreen.Show();
            }

            player.enabled = state;
        }
    }
}