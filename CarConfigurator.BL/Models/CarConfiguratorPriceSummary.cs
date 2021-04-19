using System.Collections.Generic;

namespace CarConfigurator.BL.Models
{
    public class CarConfiguratorPriceSummary
    {
        public decimal PriceBase { get; }
        public decimal PriceTotal { get; }
        public decimal PriceOptions { get; }
        public Dictionary<int, CarModelOptionProduct> ValidatedSelectedOptionProducts { get; }

        public CarConfiguratorPriceSummary(decimal priceBase, decimal priceTotal, decimal priceOptions,
            Dictionary<int, CarModelOptionProduct> validatedSelectedOptionProducts)
        {
            PriceBase = priceBase;
            PriceTotal = priceTotal;
            PriceOptions = priceOptions;
            ValidatedSelectedOptionProducts = validatedSelectedOptionProducts;
        }
    }
}