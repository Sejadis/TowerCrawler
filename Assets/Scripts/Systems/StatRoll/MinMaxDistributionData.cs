using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class MinMaxDistributionData
    {
        public MinMaxData minMaxData;
        public AnimationCurve distribution = AnimationCurve.Linear(0, 0, 1, 1);

        public float Evaluate()
        {
            var range = minMaxData.maxValue - minMaxData.minValue;
            var rangePercent = distribution.Evaluate(Random.Range(0f, 1f)) * range;
            return minMaxData.minValue + rangePercent;
        }
    }
}