using System.Collections.Generic;
using System.Linq;
using CarConfigurator.BL.Helpers;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly IProductRepository _productRepository;

        public CarModelService(IProductRepository productRepository)
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

        /// <inheritdoc />
        public CarModel GetCarModelByName(string name)
        {
            var product = _productRepository.GetByName(name);

            return MapProductToCarModel(product);
        }

        /// <summary>
        /// Maps the database products to a car model product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private static CarModel MapProductToCarModel(Product product)
        {
            if (product == null)
                return null;

            var grossPrice = PriceHelper.GetGrossPrice(product.NetPrice, product.VATRate);

            return new CarModel(product.EAN, product.Name, product.Description, grossPrice);
        }
    }
}