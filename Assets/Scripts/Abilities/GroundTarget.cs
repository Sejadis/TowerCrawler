using SejDev.Editor;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewGroundTargetAbility",
        menuName = "Systems/Ability/GroundTarget")]
    public class GroundTarget : Ability
    {
        [field: SerializeField, Rename] public float Range { get; private set; }
        [field: SerializeField, Rename] public int Size { get; private set; }
        [SerializeField] private GameObject spawnPrefab;

        private GroundTargeting groundTargeting;

        public override void Bind(IAbility abilityHandler)
        {
            base.Bind(abilityHandler);
            groundTargeting = new GroundTargeting(abilityHandler.TargetingCamera, Range, Size,
                1 << LayerMask.NameToLayer("Ground"), abilityHandler as MonoBehaviour);
        }

        protected override void PerformAbility()
        {
            if (!groundTargeting.IsTargeting)
            {
                groundTargeting.StartTargeting();
            }
            else
            {
                base.PerformAbility();
                Vector3 target = groundTargeting.GetTarget();
                Instantiate(spawnPrefab, target, Quaternion.identity);
            }
        }
    }
}