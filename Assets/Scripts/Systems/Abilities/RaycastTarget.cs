using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public class RaycastTarget : IAbilityTargeter<Vector3>
    {
        public Vector3 GetTarget()
        {
            return Vector3.zero;
        }
    }
}