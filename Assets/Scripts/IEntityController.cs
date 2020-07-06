using UnityEngine;

namespace SejDev
{
    public interface IEntityController
    {
        Vector3 MovementData { get; }
        Rigidbody RigidBody { get; }
        void WarpPosition(Vector3 direction);
    }
}