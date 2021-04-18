using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarConfiguratorProvider
    {
        IEnumerable<CarModelOption> GetCarModelsOptionsAndProducts(CarModel carModel);
    }
}