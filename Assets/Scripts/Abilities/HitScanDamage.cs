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

        public override void Bind(IAbility abilityHandler)
        {
            RaycastTargeter raycastTargeter = new RaycastTargeter(abilityHandler.TargetingCamera.transform, HitRange);
            base.Bind(abilityHandler, raycastTargeter);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            // Debug.Log($"Hit {(target ? target.name : "nothing")}");

            if (target != null)
            {
                Transform hit = target as Transform;
                IDamagable damagable = hit.GetComponent<IDamagable>();
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