using SejDev.Editor;
using SejDev.Systems.Abilities;
using SejDev.Systems.Core;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewSwordBaseAbility",
        menuName = "Systems/Ability/Sword Base Ability")]
    public partial class SwordBaseAbility : WeaponAbility
    {
        [field: SerializeField, Rename] public int Damage { get; private set; }

        protected override void TriggerHitEffect(object target)
        {
            Transform hit = target as Transform;
            IDamagable damagable = hit.GetComponent<IDamagable>();
            damagable?.TakeDamage(this, Damage);
        }
    }
}