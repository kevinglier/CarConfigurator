using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Providers
{
    public class CarConfiguratorProvider : ICarConfiguratorProvider
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;

        public CarConfiguratorProvider(IProductRepository productRepository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
        }
        
        public IEnumerable<CarModelOption> GetCarModelsOptionsAndProducts(CarModel carModel)
        {
            if (carModel == null)
                return null;

            var product = _productRepository.GetByEAN(carModel.EAN);

            if (product == null)
                return null;

            var options = this._productOptionRepository.GetProductOptionsByEAN(carModel.EAN);

            var carModelOptions = new List<CarModelOption>();

            foreach (var option in options)
            {
                var optionProducts = _productRepository.GetOptionProducts(product.Id, option.Id)
                    .Select(prod => new CarModelOptionProduct(prod.Id, prod.Name, prod.Description));

                carModelOptions.Add(new CarModelOption(option.Id, option.Name, option.Description, optionProducts));
            }
            
            return carModelOptions;
        }
    }
}