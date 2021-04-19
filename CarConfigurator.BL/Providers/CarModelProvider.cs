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

        /// <inheritdoc />
        public IEnumerable<CarModel> GetCarModels()
        {
            var products = _productRepository.GetMainProducts();

            return products.Select(MapProductToCarModel);
        }

        /// <inheritdoc />
        public CarModel GetCarModelByEAN(string ean)
        {
            var product = _productRepository.GetByEAN(ean);

            return MapProductToCarModel(product);
        }

        private static CarModel MapProductToCarModel(Product product)
        {
            var grossPrice = product.NetPrice * (1 + product.VATRate / 100);

            return new CarModel(product.EAN, product.Name, product.Description, grossPrice);
        }
    }
}