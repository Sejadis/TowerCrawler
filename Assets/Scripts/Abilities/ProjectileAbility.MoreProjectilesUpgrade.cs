using System.Collections.Generic;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    public partial class ProjectileAbility
    {
        [CreateAssetMenu(fileName = "Assets/Resources/Abilities/Upgrades//NewProjectileAbilityUpgrade",
            menuName = "Systems/Ability/Upgrades/Projectile Ability Upgrades")]
        public class MoreProjectilesUpgrade : AbilityUpgrade
        {
            public List<Vector2> spawnAngles = new List<Vector2>();

            public override void Activate()
            {
                base.Activate();
                foreach (var angle in spawnAngles)
                {
                    (ability as ProjectileAbility).spawnAngles.Add(angle);
                }
            }

            public override void DeActivate()
            {
                base.DeActivate();
                foreach (var angle in spawnAngles)
                {
                    (ability as ProjectileAbility).spawnAngles.Remove(angle);
                }
            }
        }
    }
}