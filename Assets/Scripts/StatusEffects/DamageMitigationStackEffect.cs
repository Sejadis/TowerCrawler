using System;
using SejDev.Editor;
using SejDev.Systems.Core;
using SejDev.Systems.StatusEffects;
using UnityEngine;

namespace SejDev.StatusEffects
{
    [Serializable]
    [CreateAssetMenu(fileName = "Assets/Ressources/StatusEffects/NewMitigationStackEffect", menuName = "Systems/Status Effects/MitigationStack")]
    public class DamageMitigationStackEffect : StackableStatusEffect
    {
        [field: SerializeField, Rename, Range(0, 1)]
        public float DamageMitigationPercent { get; private set; }
        IDamagable damagable;

        public override void Bind(StatusEffectManager statusEffectManager)
        {
            Bind(statusEffectManager,1);
        }

        public override void Bind(StatusEffectManager statusEffectManager, int stacks)
        {
            base.Bind(statusEffectManager);
            damagable = statusEffectManager.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.OnPreDamage += ApplyDamageMitigation;
                CurrentStacks = stacks;
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
            if (e.TryApplyMitigation(DamageMitigationPercent)){
                CurrentStacks--;
            }
        }
    }
}