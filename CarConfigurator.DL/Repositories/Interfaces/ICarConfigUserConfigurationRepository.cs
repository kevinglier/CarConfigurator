using System.Collections.Generic;
using CarConfigurator.DL.Models;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface ICarConfigUserConfigurationRepository : IRepository
    {
        CarConfigUserConfiguration Get(string code);
        CarConfigUserConfiguration Update(CarConfigUserConfiguration userConfiguration);
        CarConfigUserConfiguration Add(CarConfigUserConfiguration userConfiguration, string code);
    }
}