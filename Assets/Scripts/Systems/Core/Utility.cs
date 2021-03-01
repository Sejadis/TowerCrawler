using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SejDev.Systems.Core
{
    public static class Utility
    {
        public static T GetRandomValueFromList<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Trying to get a random value from empty list");
            }

            return list[Random.Range(0, list.Count)];
        }

        public static int GetValueIndex<T>(T enumValue) where T : Enum
        {
            var value = Convert.ToSingle(enumValue);
            return value == 0 ? 0 : (int) Mathf.Log(value, 2);
        }

        public static readonly Dictionary<Rarity, Color> RarityColors = new Dictionary<Rarity, Color>()
        {
            {Rarity.Common, Color.grey},
            {Rarity.Uncommon, Color.white},
            {Rarity.Rare, Color.blue},
            {Rarity.Epic, new Color(0.5f, 0.2f, 0.7f)},
            {Rarity.Legendary, new Color(1f, 0.3f, 0f)},
        };
    }
}