using System.Collections.Generic;
using System.Linq;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Services
{
    public class CarModelOptionService : ICarModelOptionService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;

        public CarModelOptionService(IProductRepository productRepository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
        }

        /// <inheritdoc />
        public IEnumerable<CarModelOption> GetListForModel(CarModel model)
        {
            if (model == null)
                return null;

            var products = _productOptionRepository.GetProductOptionsByEAN(model.EAN);

            return products.Select(MapProductOptionToCarModelOptions);
        }

        /// <summary>
        /// Maps an product option to a car model option
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        private static CarModelOption MapProductOptionToCarModelOptions(ProductOption productOption)
        {
            return productOption != null
                ? new CarModelOption(productOption.Id, productOption.Name, productOption.Description)
                : null;
        }
    }
}