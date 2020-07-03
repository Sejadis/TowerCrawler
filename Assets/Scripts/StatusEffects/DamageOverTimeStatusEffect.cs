using System.Collections;
using SejDev.Systems.Core;
using SejDev.Systems.StatusEffects;
using UnityEngine;

namespace SejDev.StatusEffects
{
    [CreateAssetMenu(fileName = "Assets/Ressources/StatusEffects/NewDotEffect",
        menuName = "Systems/Status Effects/DoT")]
    public class DamageOverTimeStatusEffect : StatusEffect
    {
        [SerializeField] private int ticks;
        [SerializeField] private int tickDamage;
        private float nextTick;
        private float tickDelay;
        private IDamagable damagable;

        public override void Bind(StatusEffectManager statusEffectManager)
        {
            base.Bind(statusEffectManager);
            damagable = statusEffectManager.gameObject.GetComponent<IDamagable>();
            if (damagable == null)
            {
                RemoveSelf();
                return;
            }
            else
            {
                //ticks - 1 to have the first tick on 0 and the last on max duration instead of duration - tick delay
                //5 ticks in 10s with ticks on 0|2|4|6|8 vs 0|2.5|5|7.5|10
                tickDelay = Duration / (ticks - 1);
                nextTick = applyTime;
                statusEffectManager.StartCoroutine(ApplyDoT());
            }
        }

        private float GetNextTickTime()
        {
            float next = nextTick + tickDelay;
            return next;
        }

        private IEnumerator ApplyDoT()
        {
            // int actualTicks = 0;
            while (DurationLeft >= 0)
            {
                if (nextTick <= Time.time)
                {
                    damagable.TakeDamage(this, tickDamage);
                    nextTick = GetNextTickTime();
                    // actualTicks++;
                }

                yield return null;
            }

            //TODO potentially apply last tick manually if skipped in low FPS situations
        }
    }
}