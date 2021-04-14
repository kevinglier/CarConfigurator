using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarModelProvider
    {
        IEnumerable<CarModel> GetCarModels();
        CarModel GetCarModelByName(string modelName);
    }
}