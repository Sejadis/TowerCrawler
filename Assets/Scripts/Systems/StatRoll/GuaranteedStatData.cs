using System;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class GuaranteedStatData
    {
        public StatType statType;
        public bool countsForTotalStats;
        public MinMaxDistributionData overrideData;
    }
}