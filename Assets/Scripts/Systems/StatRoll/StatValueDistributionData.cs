using System;
using SejDev.Systems.Core;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class StatValueDistributionData
    {
        public ModifierType modifierType;
        public ValueType valueType;

        [EnumNamedArray(typeof(Rarity))] public MinMaxDistributionData[] distributionData =
            new MinMaxDistributionData[Enum.GetNames(typeof(Rarity)).Length];
    }
}