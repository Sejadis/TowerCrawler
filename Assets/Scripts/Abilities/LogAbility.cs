using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Abilities/NewLogAbility", menuName = "Systems/Ability/Log Ability")]
    public class LogAbility : Ability
    {
        public override void Bind(IAbility abilityHandler)
        {
            base.Bind(abilityHandler);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            Debug.Log("Ability fired");
        }
    }
}