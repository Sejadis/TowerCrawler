﻿using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.Abilities;
using SejDev.Systems.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.UI
{
    public class UIManager : MonoBehaviour
    {
        public UIScreen abilityScreen;
        public UIScreen menuScreen;
        public UIScreen playerGameScreen;
        public GameObject player;
        private readonly List<UIScreen> activeScreens = new List<UIScreen>();

        private void Start()
        {
            InputManager.Instance.OnAbilityUI += AbilityUi;
            InputManager.Instance.OnBackUI += Back;
        }

        private void Back(InputAction.CallbackContext obj)
        {
            if (activeScreens.Count > 0)
            {
                ChangeScreenState(activeScreens[activeScreens.Count - 1], false);
            }
            else
            {
                ChangeScreenState(menuScreen, true);
                // ChangeScreenState(playerGameScreen, false);
            }
        }

        private void CheckStuff()
        {
            var uiClosed = activeScreens.Count == 0;
            player.GetComponent<MouseLook>().enabled = uiClosed;
            if (uiClosed)
            {
                InputManager.Instance.PlayerInput.Abilities.Enable();
                InputManager.Instance.PlayerInput.Controls.Enable();
                player.GetComponent<AbilityHandler>()?.ReloadAbilities();
            }
            else
            {
                InputManager.Instance.PlayerInput.Abilities.Disable();
                InputManager.Instance.PlayerInput.Controls.Disable();
            }
        }

        private void AbilityUi(InputAction.CallbackContext obj)
        {
            ChangeScreenState(abilityScreen, !abilityScreen.IsActive);
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