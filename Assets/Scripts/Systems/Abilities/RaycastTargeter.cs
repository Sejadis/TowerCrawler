using System;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public class RaycastTargeter : IAbilityTargeter
    {
        private Transform origin;
        private float range;

        public RaycastTargeter(Transform origin, float range)
        {
            this.origin = origin;
            this.range = range;
        }

        public bool IsTargeting => false;

        public bool RequiresSeperateTargeting => false;

        public void StartTargeting()
        {
            throw new InvalidOperationException(
                "Called StartTargeting on IAbilityTargeter that immediately returns target");
        }

        public object GetTarget()
        {
            Physics.Raycast(origin.position, origin.forward, out var hit, range);
            return hit.transform;
        }
    }
}