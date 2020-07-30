using UnityEngine;

namespace SejDev.Systems.Core
{
    public interface IDescribable
    {
        Sprite Icon { get; }
        string Name { get; }
        string Description { get; }
    }
}