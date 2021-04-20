using System.Dynamic;

namespace CarConfigurator.BL.Helpers
{
    public static class PriceHelper
    {
        public static decimal GetGrossPrice(decimal netPrice, decimal vatRate)
        {
            return netPrice * (1 + vatRate / 100);
        }
    }
}