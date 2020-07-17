using SejDev.Abilities.Activator;
using SejDev.Editor;
using SejDev.Player;
using SejDev.Systems.Abilities;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewWarpAbility",
        menuName = "Systems/Ability/Warp")]
    public class Warp : Ability
    {
        [field: SerializeField, Rename] public float WarpDistance { get; private set; }
        [field: SerializeField, Rename] public bool WarpInMoveDirection { get; private set; }
        [field: SerializeField, Rename] public bool AllowVerticalWarp { get; private set; } = false;
        private IEntityController controller;

        public override void Bind(IAbility abilityHandler, Stat castTime = null)
        {
            base.Bind(abilityHandler);
            controller = (abilityHandler as MonoBehaviour).GetComponent<IEntityController>();
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            // Debug.Log("Ability fired");
            Vector3 direction;
            if (WarpInMoveDirection)
            {
                var velocity = controller.FrameVelocity;
                direction =
                    (controller.FrameVelocity != Vector3.zero
                        ? controller.FrameVelocity.normalized
                        : (controller as MonoBehaviour).transform.forward) * WarpDistance;
            }
            else
            {
                direction = (controller as MonoBehaviour).transform.forward * WarpDistance;
            }

            if (!AllowVerticalWarp)
            {
                direction.y = -5f;
            }

            controller.WarpPosition(direction);
        }
    }
}