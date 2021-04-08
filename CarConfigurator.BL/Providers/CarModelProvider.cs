using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Providers
{
    public class CarModelProvider : ICarModelProvider
    {
        private readonly IProductRepository _productRepository;

        public CarModelProvider(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<CarModel> GetCarModels(bool withAvailableOptions)
        {
            var products = _productRepository.GetMainProducts();

            return products.Select(
                prod =>
                    new CarModel(prod.Name, prod.Description)
            );
        }
    }
}