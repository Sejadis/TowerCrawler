using System;
using System.Collections.Generic;
using UnityEngine;

namespace SejDev.Abilities
{
    public partial class ProjectileAbility
    {
        [Serializable]
        public class MoreProjectilesUpgrade
        {
            private ProjectileAbility ability;
            public List<Vector2> spawnAngles = new List<Vector2>();

            public void Bind(ProjectileAbility ability)
            {
                this.ability = ability;
            }

            public void Activate()
            {
                foreach (var angle in spawnAngles)
                {
                    ability.spawnAngles.Add(angle);
                }
            }

            public void DeActivate()
            {
                foreach (var angle in spawnAngles)
                {
                    ability.spawnAngles.Remove(angle);
                }
            }
        }
    }
}