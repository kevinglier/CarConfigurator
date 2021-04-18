using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarModelOptionProvider
    {
        IEnumerable<CarModelOption> GetListForModel(CarModel model);
    }
}