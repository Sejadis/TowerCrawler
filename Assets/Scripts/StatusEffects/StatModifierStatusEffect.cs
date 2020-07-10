using SejDev.Systems.Stats;
using SejDev.Systems.StatusEffects;
using UnityEngine;

namespace SejDev.StatusEffects
{
    [CreateAssetMenu(fileName = "Assets/Resources/StatusEffects/NewStatModifierEffect",
        menuName = "Systems/Status Effects/Stat Modifier")]
    public class StatModifierStatusEffect : StatusEffect
    {
        IStats stats;
        [SerializeField] private StatType affectedStat;
        [SerializeField] private Modifier modifier;


        public override void Bind(StatusEffectManager statusEffectManager)
        {
            base.Bind(statusEffectManager);
            stats = statusEffectManager.gameObject.GetComponent<IStats>();
            if (stats == null)
            {
                RemoveSelf();
                return;
            }
            else
            {
                stats.AddModifier(affectedStat, modifier);
            }
        }

        public override void UnBind()
        {
            base.UnBind();
            stats.RemoveModifier(affectedStat, modifier);
        }
    }
}