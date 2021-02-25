using System;
using SejDev.Systems.Equipment;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class SlotRarityDistributionData
    {
        [EnumNamedArray(typeof(EquipSlotType))]
        public RarityDistributionData[] distributionData =
            new RarityDistributionData[Enum.GetNames(typeof(EquipSlotType)).Length];


        public void Validate()
        {
            var enumLength = Enum.GetNames(typeof(EquipSlotType)).Length;
            if (distributionData.Length != enumLength)
            {
                var array = new RarityDistributionData[enumLength];
                for (int i = 0; i < distributionData.Length && i < enumLength; i++)
                {
                    array[i] = distributionData[i];
                }

                distributionData = array;
            }
        }
    }
}