﻿using System;
using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.UI
{
    public class UIManager : MonoBehaviour
    {
        public UIScreen upgradeScreen;
        public UIScreen abilityScreen;
        public MouseLook player;
        List<UIScreen> activeScreens = new List<UIScreen>();

        private void Start()
        {
            InputManager.Instance.OnUpgradeUI += UpgradeUi;
            InputManager.Instance.OnAbilityUI += AbilityUi;
        }

        private void CheckStuff()
        {
            player.enabled = activeScreens.Count == 0;
        }

        private void AbilityUi(InputAction.CallbackContext obj)
        {
            ChangeScreenState(abilityScreen, !abilityScreen.IsActive);
        }

        private void UpgradeUi(InputAction.CallbackContext obj)
        {
            ChangeScreenState(upgradeScreen, !upgradeScreen.IsActive);
        }

        private void ChangeScreenState(UIScreen screen, bool newState)
        {
            if (newState)
            {
                screen.Show();
                activeScreens.Add(screen);
            }
            else
            {
                screen.Hide();
                activeScreens.Remove(screen);
            }

            CheckStuff();
        }
    }
}