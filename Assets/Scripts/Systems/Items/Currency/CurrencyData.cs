using SejDev.Editor;
using SejDev.Systems.Core;
using UnityEngine;

namespace SejDev.Systems.Equipment
{
    [CreateAssetMenu(fileName = "Assets/Resources/Items/Currency/NewCurrency",
        menuName = "Systems/Items/Currency")]
    public class CurrencyData : Item
    {
        [field: Rename, SerializeField] public Rarity Rarity { get; private set; }
    }
}