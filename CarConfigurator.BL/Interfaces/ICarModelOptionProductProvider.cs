using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarModelOptionProductProvider
    {
        IEnumerable<CarModel> GetProductForOption(CarModelOption option);
    }
}