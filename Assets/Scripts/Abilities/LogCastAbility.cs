using SejDev.Abilities.Activator;
using SejDev.Editor;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Abilities/NewLogCastAbility",
        menuName = "Systems/Ability/Log CastAbility")]
    public class LogCastAbility : Ability
    {

        public override void Bind(AbilityManager abilityManager)
        {
            base.Bind(abilityManager);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            Debug.Log("Ability fired");
            Ping ping = new Ping("123");
            
        }
    }
}