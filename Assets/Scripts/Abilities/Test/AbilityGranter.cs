using System;
using UnityEngine;
using SejDev.Systems.Abilities;

namespace SejDev.Abilities.Test
{
    public class AbilityGranter : MonoBehaviour
    {
        public Ability ability;

        private void OnTriggerEnter(Collider other)
        {
            IAbility abilityManager = other.gameObject.GetComponent<IAbility>();
            if (abilityManager != null)
            {
                abilityManager.ChangeAbility(ability,AbilitySlot.Core1);
            }
        }
    }
}