﻿using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface ICarModelProvider
    {
        IEnumerable<CarModel> GetCarModels(bool withAvailableOptions);
        CarModel GetCarModelByName(string modelName, bool withAvailableOptions);
    }
}