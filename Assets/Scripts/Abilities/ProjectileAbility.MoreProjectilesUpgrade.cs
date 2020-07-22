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

            protected override void Activate()
            {
                foreach (var angle in spawnAngles)
                {
                    (ability as ProjectileAbility).spawnAngles.Add(angle);
                }
            }

            protected override void DeActivate()
            {
                foreach (var angle in spawnAngles)
                {
                    (ability as ProjectileAbility).spawnAngles.Remove(angle);
                }
            }
        }
    }
}