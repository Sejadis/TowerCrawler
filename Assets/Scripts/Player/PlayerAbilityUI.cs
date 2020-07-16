using System;
using SejDev.Systems.Abilities;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.UI
{
    public class PlayerAbilityUI : MonoBehaviour
    {
        [SerializeField] private AbilityHolderUI weaponBase;
        [SerializeField] private AbilityHolderUI weaponSpecial;
        [SerializeField] private AbilityHolderUI core1;
        [SerializeField] private AbilityHolderUI core2;
        [SerializeField] private AbilityHolderUI core3;
        [SerializeField] private AbilityManager abilityManager;

        private void Start()
        {
            weaponBase.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.WeaponBase));
            weaponBase.Bind(InputManager.Instance.PlayerInput.Abilities.WeaponBase.bindings[0]);

            weaponSpecial.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.WeaponSpecial));
            weaponSpecial.Bind(InputManager.Instance.PlayerInput.Abilities.WeaponSpecial.bindings[0]);

            core1.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.Core1));
            core1.Bind(InputManager.Instance.PlayerInput.Abilities.Core1.bindings[0]);

            core2.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.Core2));
            core2.Bind(InputManager.Instance.PlayerInput.Abilities.Core2.bindings[0]);

            core3.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.Core3));
            core3.Bind(InputManager.Instance.PlayerInput.Abilities.Core3.bindings[0]);
            abilityManager.OnPostAbilityChanged += OnPostAbilityChanged;
        }

        private void OnDestroy()
        {
            abilityManager.OnPostAbilityChanged -= OnPostAbilityChanged;
        }

        private void OnPostAbilityChanged(object sender, AbilityChangedEventArgs e)
        {
            switch (e.slot)
            {
                case AbilitySlot.WeaponBase:
                    weaponBase.Bind(e.ability);
                    break;
                case AbilitySlot.WeaponSpecial:
                    weaponSpecial.Bind(e.ability);
                    break;
                case AbilitySlot.Core1:
                    core1.Bind(e.ability);
                    break;
                case AbilitySlot.Core2:
                    core2.Bind(e.ability);
                    break;
                case AbilitySlot.Core3:
                    core3.Bind(e.ability);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}