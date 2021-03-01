namespace SejDev.Systems.Equipment
{
    public class Currency
    {
        public Currency(CurrencyData currencyData, int amount)
        {
            CurrencyData = currencyData;
            Amount = amount;
        }

        public CurrencyData CurrencyData { get; set; }
        public int Amount { get; set; }
    }
}