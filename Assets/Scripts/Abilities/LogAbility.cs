using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Abilities/NewLogAbility", menuName = "Systems/Ability/Log Ability")]
    public class LogAbility : Systems.Abilities.Ability
    {
        public override void Bind(AbilityManager abilityManager)
        {
            base.Bind(abilityManager);
        }

        public override void Activate()
        {
            base.Activate();
            Debug.Log("Ability fired");
        }
    }

}