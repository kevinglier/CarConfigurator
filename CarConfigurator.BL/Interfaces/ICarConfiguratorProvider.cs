using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarConfiguratorProvider
    {
        IEnumerable<CarModelOption> GetCarModelsOptionsAndProducts(CarModel carModel);
        CarConfiguratorPriceSummary GetSummaryForSelectedOptionProducts(string carModelEAN, Dictionary<int, CarModelOptionProduct> selectedOptionProducts);
        IEnumerable<CarModelOptionProduct> GetCarModelsOptionProducts(int carModelProductId, int optionId);
        CarConfiguratorPriceSummary SaveConfiguration(string code = null);
    }
}