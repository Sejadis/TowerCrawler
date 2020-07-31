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
        private readonly List<UIScreen> activeScreens = new List<UIScreen>();

        private void Start()
        {
            InputManager.Instance.OnAbilityUI += AbilityUi;
            InputManager.Instance.OnBackUI += Back;
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