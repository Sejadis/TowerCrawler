using UnityEngine;

namespace SejDev.Systems.Stats
{
    [CreateAssetMenu(fileName = "Assets/Ressources/Stats/NewStatRestrictor",
        menuName = "Systems/Stats/Stat Restrictor")]
    public class StatRestrictor : ScriptableObject
    {
        public float maxPercent;
        public float minPercent;
    }
}