using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Providers
{
    public class CarConfiguratorProvider : ICarConfiguratorProvider
    {
        private readonly ICarModelProvider _carModelProvider;
        private readonly ICarModelOptionProvider _carModelOptionProvider;
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly ICarConfigUserConfigurationRepository _carConfigUserConfigurationRepository;

        public CarConfiguratorProvider(
            ICarModelProvider carModelProvider,
            ICarModelOptionProvider carModelOptionProvider,
            IProductRepository productRepository,
            IProductOptionRepository productOptionRepository,
            ICarConfigUserConfigurationRepository carConfigUserConfigurationRepository)
        {
            _carModelProvider = carModelProvider;
            _carModelOptionProvider = carModelOptionProvider;
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
            _carConfigUserConfigurationRepository = carConfigUserConfigurationRepository;
        }

        /// <summary>
        /// Gibt die Modell-Optionen sowie deren Produkte zurück
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns></returns>
        public IEnumerable<CarModelOption> GetCarModelsOptionsAndProducts(CarModel carModel)
        {
            if (carModel == null)
                return null;

            var carModelProduct = _productRepository.GetByEAN(carModel.EAN);

            if (carModelProduct == null)
                return null;

            var options = this._productOptionRepository.GetProductOptionsByEAN(carModel.EAN);

            var carModelOptions = new List<CarModelOption>();

            // Produkte zu den Optionen auslesen und an das CarModelOption-Object anhängen
            foreach (var option in options)
            {
                var optionProducts = this.GetCarModelsOptionProducts(carModelProduct.Id, option.Id);

                carModelOptions.Add(new CarModelOption(option.Id, option.Name, option.Description, optionProducts));
            }

            return carModelOptions;
        }

        public IEnumerable<CarModelOptionProduct> GetCarModelsOptionProducts(int carModelProductId, int optionId)
        {
            var option = _productOptionRepository.GetById(optionId, carModelProductId);
            if (option == null)
                return null;

            var products = _productRepository.GetOptionProducts(carModelProductId, optionId)
                .Select(optionProduct =>
                {
                    // Wenn es sich um ein Standard-Produkt handelt, ist der Preis im Preis des Fahrzeugs enthalten
                    var grossPrice = option.DefaultProductIds.Contains(optionProduct.Id)
                        ? 0
                        : optionProduct.NetPrice * (1 + optionProduct.VATRate / 100);

                    return new CarModelOptionProduct(optionProduct.EAN, optionProduct.Name,
                        optionProduct.Description, grossPrice,
                        option.DefaultProductIds != null && option.DefaultProductIds.Contains(optionProduct.Id));
                })
                .OrderBy(x => x.Price);

            return products;
        }

        public Dictionary<int, CarModelOptionProduct> GetSavedUserConfiguration(string code)
        {
            var userConfiguration = _carConfigUserConfigurationRepository.Get(code);
            if (userConfiguration == null)
                throw new Exception("The code is unknown.");

            var model = _carModelProvider.GetCarModelByEAN(userConfiguration.ModelEAN);
            var optionsAndGroups = _carModelOptionProvider.GetListForModel(model);

            var selectedProducts = new Dictionary<int, CarModelOptionProduct>();
            foreach (var userProduct in userConfiguration.Products)
            {
                var product = _productRepository.GetById(userProduct.SelectedOptionProductId);

                selectedProducts.Add(userProduct.OptionId, new CarModelOptionProduct(
                    product.EAN, product.Name, product.Description, product.NetPrice * (1 / product.VATRate), false)
                );
            }

            return selectedProducts;
        }

        private CarConfigUserConfiguration SaveConfiguration(CarConfiguratorSummary summary)
        {
            var carModelProduct = _productRepository.GetByEAN(summary.SelectedModelEAN);

            if (carModelProduct == null || carModelProduct.IsOptionProduct)
                throw new Exception("Unknown car model.");

            var configurationProducts = summary.SelectedOptionProducts.Select(keyValuePair =>
            {
                var optionId = keyValuePair.Key;
                var selectedProduct = keyValuePair.Value;

                var product = _productRepository.GetByEAN(selectedProduct.EAN);
                return new CarConfigUserConfigurationProduct(carModelProduct.Id, optionId, product.Id);
            }).ToList();

            CarConfigUserConfiguration userConfiguration;
            if (summary.Code != null)
            {
                userConfiguration = _carConfigUserConfigurationRepository.Get(summary.Code);
                userConfiguration.Products = configurationProducts;

                userConfiguration = _carConfigUserConfigurationRepository.Update(userConfiguration);

                return userConfiguration;
            }

            var code = Guid.NewGuid().ToString("N");

            userConfiguration = new CarConfigUserConfiguration(
                carModelProduct.EAN,
                configurationProducts
            );

            userConfiguration = _carConfigUserConfigurationRepository.Add(userConfiguration, code);

            return userConfiguration;
        }

        public CarConfiguratorSummary GetSummaryForSelectedOptionProducts(string carModelEAN,
            Dictionary<int, CarModelOptionProduct> selectedOptionProducts, string code = null)
        {
            if (carModelEAN == null)
                throw new Exception("Car model ean missing.");

            if (selectedOptionProducts == null || selectedOptionProducts.Count == 0)
                throw new Exception("Selected car model option products missing.");

            var carModelProduct = _productRepository.GetByEAN(carModelEAN);
            if (carModelProduct == null)
                throw new Exception("Unknown car model.");

            decimal basePrice = carModelProduct.NetPrice * (1 + carModelProduct.VATRate / 100);
            decimal optionsPrice = 0;

            var validatedSelectedOptionProducts = new Dictionary<int, CarModelOptionProduct>();

            var modelAvailableOptions = _productOptionRepository.GetProductOptionsByEAN(carModelEAN);

            foreach (var availableOption in modelAvailableOptions)
            {
                string selectedEAN;

                selectedEAN = selectedOptionProducts[availableOption.Id] != null
                    ? selectedOptionProducts[availableOption.Id].EAN
                    : _productRepository.GetProductsByIds(availableOption.DefaultProductIds).Select(x => x.EAN)
                        .FirstOrDefault();

                var userSelectedProduct = _productRepository.GetByEAN(selectedEAN);
                if (userSelectedProduct == null)
                    throw new Exception("Unknown product with EAN " + selectedEAN + ".");

                var availableProductsForOptions =
                    GetCarModelsOptionProducts(carModelProduct.Id, availableOption.Id);
                // _productRepository.GetOptionProducts(carModelProduct.Id, availableOption.Id);


                var userSelectedOptionProduct =
                    availableProductsForOptions.FirstOrDefault(prod => prod.EAN == selectedEAN);

                // If the product is not an product of the group for the car model
                if (userSelectedOptionProduct == null)
                    continue;


                validatedSelectedOptionProducts.Add(availableOption.Id, userSelectedOptionProduct);

                // If it is a default product, the product price is included in the car's base price
                if (availableOption.DefaultProductIds.Contains(userSelectedProduct.Id))
                    continue;

                optionsPrice += userSelectedProduct.NetPrice * (1 + userSelectedProduct.VATRate / 100);
            }

            var totalPrice = basePrice + optionsPrice;

            var summary =
                new CarConfiguratorSummary(code, basePrice, totalPrice, optionsPrice, carModelEAN,
                    validatedSelectedOptionProducts);

            var userConfiguration = SaveConfiguration(summary);

            summary.Code = userConfiguration.Code;

            return summary;
        }
    }
}