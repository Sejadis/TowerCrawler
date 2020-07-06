using SejDev.Abilities.Activator;
using SejDev.Editor;
using SejDev.Player;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Abilities/NewWarpAbility",
        menuName = "Systems/Ability/Warp")]
    public class Warp : Ability
    {
        [field: SerializeField, Rename] public float WarpDistance { get; private set; }
        [field: SerializeField, Rename] public bool WarpInMoveDirection { get; private set; }
        private IEntityController controller;

        public override void Bind(AbilityManager abilityManager)
        {
            base.Bind(abilityManager);
            controller = abilityManager.GetComponent<IEntityController>();
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            // Debug.Log("Ability fired");
            Vector3 direction;
            if (WarpInMoveDirection)
            {
                direction =
                    (controller.RigidBody.velocity != Vector3.zero
                        ? controller.RigidBody.velocity.normalized
                        : controller.RigidBody.transform.forward) * (WarpDistance * 10);
            }
            else
            {
                direction = controller.RigidBody.transform.forward * (WarpDistance * 10);
            }

            controller.WarpPosition(direction);
        }
    }
}