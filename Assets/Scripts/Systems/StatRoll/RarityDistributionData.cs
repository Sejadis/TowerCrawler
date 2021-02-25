using System;
using SejDev.Systems.Core;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class RarityDistributionData
    {
        [EnumNamedArray(typeof(Rarity))]
        public MinMaxData[] distributionData = new MinMaxData[Enum.GetNames(typeof(Rarity)).Length];
    }
}