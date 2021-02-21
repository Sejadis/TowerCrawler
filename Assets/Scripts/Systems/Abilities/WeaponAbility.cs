using System;
using JetBrains.Annotations;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;

namespace SejDev.Systems.Abilities
{
    public abstract class WeaponAbility : Ability
    {
        protected WeaponController weaponControllerInstance;

        public virtual void Bind([NotNull] IAbility abilityHandler, [NotNull] WeaponController weaponHandler,
            Stat castTime = null)
        {
            weaponControllerInstance = weaponHandler;
            weaponControllerInstance.HitEffect = TriggerHitEffect;
            base.Bind(abilityHandler, castTime);
        }

        protected sealed override void PerformAbility()
        {
            base.PerformAbility();
            weaponControllerInstance.TriggerAttack();
        }

        protected abstract void TriggerHitEffect(object target);

        public override void Bind(IAbility abilityHandler, Stat castTime = null)
        {
            throw new Exception(
                "This method is not suitable for weapon abilities, use the one with weaponhandler instead");
        }
    }
}