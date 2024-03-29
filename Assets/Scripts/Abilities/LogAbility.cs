﻿using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewLogAbility", menuName = "Systems/Ability/Log Ability")]
    public class LogAbility : Ability
    {
        protected override void PerformAbility()
        {
            base.PerformAbility();
            Debug.Log("Ability fired");
        }
    }
}