using System;
using SejDev.Systems.Abilities;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.UI
{
    public class PlayerAbilityUI : MonoBehaviour
    {
        [SerializeField] private AbilitySlot weaponBase;
        [SerializeField] private AbilitySlot weaponSpecial;
        [SerializeField] private AbilitySlot core1;
        [SerializeField] private AbilitySlot core2;
        [SerializeField] private AbilitySlot core3;
        [SerializeField] private AbilityHandler abilityHandler;

        private void Start()
        {
            weaponBase.Bind(abilityHandler.GetAbilityBySlot(Systems.Abilities.AbilitySlot.WeaponBase));
            weaponBase.Bind(InputManager.Instance.PlayerInput.Abilities.WeaponBase.bindings[0]);

            weaponSpecial.Bind(abilityHandler.GetAbilityBySlot(Systems.Abilities.AbilitySlot.WeaponSpecial));
            weaponSpecial.Bind(InputManager.Instance.PlayerInput.Abilities.WeaponSpecial.bindings[0]);

            core1.Bind(abilityHandler.GetAbilityBySlot(Systems.Abilities.AbilitySlot.Core1));
            core1.Bind(InputManager.Instance.PlayerInput.Abilities.Core1.bindings[0]);

            core2.Bind(abilityHandler.GetAbilityBySlot(Systems.Abilities.AbilitySlot.Core2));
            core2.Bind(InputManager.Instance.PlayerInput.Abilities.Core2.bindings[0]);

            core3.Bind(abilityHandler.GetAbilityBySlot(Systems.Abilities.AbilitySlot.Core3));
            core3.Bind(InputManager.Instance.PlayerInput.Abilities.Core3.bindings[0]);
            abilityHandler.OnPostAbilityChanged += OnPostAbilityChanged;
        }

        private void OnDestroy()
        {
            abilityHandler.OnPostAbilityChanged -= OnPostAbilityChanged;
        }

        private void OnPostAbilityChanged(object sender, AbilityChangedEventArgs e)
        {
            switch (e.slot)
            {
                case Systems.Abilities.AbilitySlot.WeaponBase:
                    weaponBase.Bind(e.ability);
                    break;
                case Systems.Abilities.AbilitySlot.WeaponSpecial:
                    weaponSpecial.Bind(e.ability);
                    break;
                case Systems.Abilities.AbilitySlot.Core1:
                    core1.Bind(e.ability);
                    break;
                case Systems.Abilities.AbilitySlot.Core2:
                    core2.Bind(e.ability);
                    break;
                case Systems.Abilities.AbilitySlot.Core3:
                    core3.Bind(e.ability);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}