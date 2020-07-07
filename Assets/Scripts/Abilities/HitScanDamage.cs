using SejDev.Editor;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Abilities/NewHitScanAbility",
        menuName = "Systems/Ability/HitScan")]
    public class HitScanDamage : Ability
    {
        [field: SerializeField, Rename] public float WarpDistance { get; private set; }
        private RaycastTarget raycastTarget;

        public override void Bind(IAbility abilityHandler)
        {
            base.Bind(abilityHandler);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            Debug.Log("use a charge");
        }
    }
}