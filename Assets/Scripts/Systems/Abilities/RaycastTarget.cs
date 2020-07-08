using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public class RaycastTarget
    {
        private Transform origin;
        private float range;

        public RaycastTarget(Transform origin, float range)
        {
            this.origin = origin;
            this.range = range;
        }

        public Transform GetTarget()
        {
            Physics.Raycast(origin.position, origin.forward, out var hit, range);
            return hit.transform;
        }
    }
}