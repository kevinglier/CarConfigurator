using System.Collections.Generic;
using System.Linq;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Providers
{
    public class CarModelOptionProvider : ICarModelOptionProvider
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;

        public CarModelOptionProvider(IProductRepository productRepository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
        }

        public IEnumerable<CarModel> GetOptionsForModel(CarModel model)
        {
            var products = _productOptionRepository.GetProductOptions(model.ProductId);

            return products.Select(MapProductOptionToCarModelOptions);
        }

        private static CarModel MapProductOptionToCarModelOptions(ProductOption productOption)
        {
            return productOption != null ? new CarModel(productOption.Id, productOption.Name, productOption.Description) : null;
        }
    }
}