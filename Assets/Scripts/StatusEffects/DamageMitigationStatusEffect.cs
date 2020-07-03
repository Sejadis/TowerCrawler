using System;
using SejDev.Editor;
using SejDev.Systems.Core;
using SejDev.Systems.StatusEffects;
using UnityEngine;

namespace SejDev.StatusEffects
{
    [Serializable]
    [CreateAssetMenu(fileName = "Assets/Ressources/StatusEffects/NewMitigationEffect", menuName = "Systems/Status Effects/Mitigation")]
    public class DamageMitigationStatusEffect : StatusEffect
    {
        [field: SerializeField, Rename, Range(0, 1)]
        public float DamageMitigationPercent { get; private set; }
        IDamagable damagable;

        public override void Bind(StatusEffectManager statusEffectManager)
        {
            base.Bind(statusEffectManager);
            damagable = statusEffectManager.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.OnPreDamage += ApplyDamageMitigation;
            }
            else
            {
                RemoveSelf();
            }
        }

        public override void UnBind()
        {
            if (damagable != null)
            {
                damagable.OnPreDamage -= ApplyDamageMitigation;
            }
        }

        private void ApplyDamageMitigation(object sender, DamageHandlerEventArgs e)
        {
            e.TryApplyMitigation(DamageMitigationPercent);
        }
    }
}