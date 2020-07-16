using System;
using UnityEngine;

namespace SejDev
{
    public interface IEntityController
    {
        bool IsMoving { get; }
        Vector3 FrameVelocity { get; }
        event EventHandler<bool> OnMoveStateChanged;
        void WarpPosition(Vector3 direction);
    }
}