using System;
using SejDev.Systems.Equipment;

namespace SejDev.Save
{
    [Serializable]
    public class CurrencySave
    {
        public readonly string guid;
        public readonly int amount;

        public CurrencySave(Currency currency)
        {
            guid = currency.CurrencyData.GUID;
            amount = currency.Amount;
        }

        public CurrencySave(string guid, int amount)
        {
            this.guid = guid;
            this.amount = amount;
        }
    }
}