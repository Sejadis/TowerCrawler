using SejDev.Systems.Core;
using SejDev.Systems.StatusEffects;
using UnityEngine;

namespace SejDev.StatusEffects
{
    [CreateAssetMenu(fileName = "Assets/Ressources/StatusEffects/NewThornsEffect", menuName = "Systems/Status Effects/Thorns")]
    public class ThornsStatusEffect : StatusEffect
    {
        [SerializeField, Range(0, 1)]
        protected float damageReflectionPercent;
        IDamagable damagable;

        public ThornsStatusEffect(float duration, float damageReflectionPercent)
        {
            Duration = duration;
            this.damageReflectionPercent = damageReflectionPercent;
            Name = "Thorns";
        }

        public override void Bind(StatusEffectManager statusEffectManager)
        {
            base.Bind(statusEffectManager);
            damagable = statusEffectManager.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.OnPostDamage += ApplyThorns;
                // statusEffectManager.StartCoroutine(Update());
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
                damagable.OnPostDamage -= ApplyThorns;
            }
        }

        private void ApplyThorns(object sender, DamageHandlerEventArgs e)
        {
            if (!e.resultedInDeath)
            {
                if (e.damageSource is MonoBehaviour)
                {
                    var damagable = ((MonoBehaviour)e.damageSource).gameObject.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        int reflectedDamage = (int)(e.finalDamage * damageReflectionPercent);
                        reflectedDamage = Mathf.Max(reflectedDamage, 1);
                        damagable.TakeDamage(this, reflectedDamage);
                    }
                }
            }
        }
    }
}