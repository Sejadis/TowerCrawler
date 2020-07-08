using SejDev.Editor;
using SejDev.Systems.Abilities;
using SejDev.Systems.Core;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewHitScanAbility",
        menuName = "Systems/Ability/HitScan")]
    public class HitScanDamage : Ability
    {
        [field: SerializeField, Rename] public float HitRange { get; private set; }
        [field: SerializeField, Rename] public int Damage { get; private set; }
        private RaycastTarget raycastTarget;

        public override void Bind(IAbility abilityHandler)
        {
            base.Bind(abilityHandler);
            raycastTarget = new RaycastTarget(abilityHandler.TargetingCamera.transform, HitRange);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            Transform target = raycastTarget.GetTarget();
            // Debug.Log($"Hit {(target ? target.name : "nothing")}");
            if (target != null)
            {
                IDamagable damagable = target.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(this, Damage);
                    // GameObject lineObj = new GameObject();
                    // LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
                    // lineRenderer.SetPositions(new Vector3[] {abilityManager.TargetingCamera.transform.position, target.position});
                    // Destroy(lineObj,1.5f);
                }
            }
        }
    }
}