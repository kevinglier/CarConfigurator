using System.Collections.Generic;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarConfiguratorProvider
    {
        IEnumerable<CarModelOption> GetCarModelsOptionsAndProducts(CarModel carModel);
        CarConfiguratorSummary GetSummaryForSelectedOptionProducts(string carModelEAN, Dictionary<int, CarModelOptionProduct> selectedOptionProducts, string code = null);
        IEnumerable<CarModelOptionProduct> GetCarModelsOptionProducts(int carModelProductId, int optionId);
        Dictionary<int, CarModelOptionProduct> GetSavedUserConfiguration(string code);
    }
}