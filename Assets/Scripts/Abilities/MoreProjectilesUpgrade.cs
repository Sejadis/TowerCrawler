using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/Upgrades//NewProjectileAbilityUpgrade",
        menuName = "Systems/Ability/Upgrades/Projectile Ability Upgrades")]
    public class MoreProjectilesUpgrade : AbilityUpgrade
    {
        public ProjectileAbility.MoreProjectilesUpgrade upgrade = new ProjectileAbility.MoreProjectilesUpgrade();

        public override void Bind(Ability ability)
        {
            upgrade.Bind(ability as ProjectileAbility);
            base.Bind(ability);
        }

        protected override void Activate()
        {
            upgrade.Activate();
        }

        protected override void DeActivate()
        {
            upgrade.DeActivate();
        }
    }
}