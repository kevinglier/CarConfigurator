using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
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

        public IEnumerable<CarModel> GetCarModels()
        {
            var products = _productRepository.GetMainProducts();

            return products.Select(MapProductToCarModel);
        }

        public CarModel GetCarModelByName(string modelName)
        {
            var product = _productRepository.GetByName(modelName);

            return MapProductToCarModel(product);
        }

        private static CarModel MapProductToCarModel(Product product)
        {
            return product != null ? new CarModel(product.Id, product.Name, product.Description) : null;
        }
    }
}