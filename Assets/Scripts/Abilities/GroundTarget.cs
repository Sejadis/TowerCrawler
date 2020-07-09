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

        public override void Bind(IAbility abilityHandler)
        {
            GroundTargeting groundTargeting = new GroundTargeting(abilityHandler.TargetingCamera, Range, Size,
                1 << LayerMask.NameToLayer("Ground"), abilityHandler as MonoBehaviour);
            base.Bind(abilityHandler, groundTargeting);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            Instantiate(spawnPrefab, (Vector3) target, Quaternion.identity);
        }
    }
}