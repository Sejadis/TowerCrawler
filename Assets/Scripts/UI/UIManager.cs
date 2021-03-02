using System.Collections.Generic;
using SejDev.Systems.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.UI
{
    public class UIManager : MonoBehaviour
    {
        public UIScreen abilityScreen;
        public UIScreen menuScreen;
        public UIScreen inventoryScreen;
        public UIScreen craftingScreen;
        private readonly List<UIScreen> activeScreens = new List<UIScreen>();

        private void Start()
        {
            ChangeScreenState(abilityScreen, false);
            ChangeScreenState(menuScreen, false);
            ChangeScreenState(inventoryScreen, false);
            ChangeScreenState(craftingScreen, false);

            InputManager.Instance.OnAbilityUI += AbilityUi;
            InputManager.Instance.OnBackUI += Back;
            InputManager.Instance.OnInventoryUI += InventoryUI;
            InputManager.Instance.OnCraftingUI += CraftingUI;
        }

        private void CraftingUI(InputAction.CallbackContext obj)
        {
            ChangeScreenState(craftingScreen, !craftingScreen.IsActive);
        }

        private void InventoryUI(InputAction.CallbackContext obj)
        {
            ChangeScreenState(inventoryScreen, !inventoryScreen.IsActive);
        }

        private void AbilityUi(InputAction.CallbackContext obj)
        {
            ChangeScreenState(abilityScreen, !abilityScreen.IsActive);
        }

        private void Back(InputAction.CallbackContext obj)
        {
            if (activeScreens.Count > 0)
            {
                var screen = activeScreens[activeScreens.Count - 1];
                ChangeScreenState(screen, !screen.IsActive);
            }
            else
            {
                ChangeScreenState(menuScreen, true);
            }
        }

        private void ChangeScreenState(UIScreen screen, bool newState)
        {
            if (newState)
            {
                activeScreens.Add(screen);
                screen.Show();
            }
            else
            {
                activeScreens.Remove(screen);
                screen.Hide();
            }

            CheckStuff();
        }

//TODO rename
        private void CheckStuff()
        {
            var uiClosed = activeScreens.Count == 0;
            if (uiClosed)
            {
                GameManager.Instance.EnablePlayer();
            }
            else
            {
                GameManager.Instance.DisablePlayer();
            }
        }
    }
}