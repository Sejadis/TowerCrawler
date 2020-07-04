using System;
using SejDev.Systems.Abilities;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.UI
{
    public class PlayerAbilityUI : MonoBehaviour
    {
        [SerializeField]private AbilityHolderUI weaponBase;
        [SerializeField]private AbilityHolderUI weaponSpecial;
        [SerializeField]private AbilityHolderUI core1;
        [SerializeField]private AbilityHolderUI core2;
        [SerializeField]private AbilityHolderUI core3;
        [SerializeField] private AbilityManager abilityManager;

        private void Start()
        {
            weaponBase.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.WeaponBase));
            weaponSpecial.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.WeaponSpecial));
            core1.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.Core1));
            core2.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.Core2));
            core3.Bind(abilityManager.GetAbilityBySlot(AbilitySlot.Core3));
            abilityManager.OnPostAbilityChanged += OnPostAbilityChanged;
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