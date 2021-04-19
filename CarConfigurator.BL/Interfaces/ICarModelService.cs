using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModel> GetCarModels();
        CarModel GetCarModelByEAN(string ean);

        /// <inheritdoc />
        CarModel GetCarModelByName(string name);
    }
}