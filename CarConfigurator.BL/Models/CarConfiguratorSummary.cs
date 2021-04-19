using System.Collections.Generic;

namespace CarConfigurator.BL.Models
{
    public class CarConfiguratorSummary
    {
        public decimal PriceBase { get; }
        public decimal PriceTotal { get; }
        public decimal PriceOptions { get; }
        public Dictionary<int, CarModelOptionProduct> SelectedOptionProducts { get; }
        public string Code { get; set; }
        public string SelectedModelEAN { get; set; }

        public CarConfiguratorSummary(string code, decimal priceBase, decimal priceTotal, decimal priceOptions,
            string selectedModelEAN, Dictionary<int, CarModelOptionProduct> selectedOptionProducts)
        {
            Code = code;
            PriceBase = priceBase;
            PriceTotal = priceTotal;
            PriceOptions = priceOptions;
            SelectedModelEAN = selectedModelEAN;
            SelectedOptionProducts = selectedOptionProducts;
        }
    }
}