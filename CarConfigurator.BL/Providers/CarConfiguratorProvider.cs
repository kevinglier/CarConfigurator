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
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;

        public CarConfiguratorProvider(IProductRepository productRepository,
            IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
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

        public CarConfiguratorPriceSummary SaveConfiguration(string code = null)
        {
            throw new NotImplementedException();
        }

        public CarConfiguratorPriceSummary GetSummaryForSelectedOptionProducts(string carModelEAN,
            Dictionary<int, CarModelOptionProduct> selectedOptionProducts)
        {
            if (selectedOptionProducts == null || selectedOptionProducts.Count == 0 || carModelEAN == null)
                return null;

            var carModelProduct = _productRepository.GetByEAN(carModelEAN);
            if (carModelProduct == null)
                throw new Exception("Unknown car model.");

            decimal basePrice = carModelProduct.NetPrice * (1 + carModelProduct.VATRate / 100);
            decimal optionsPrice = 0;

            var validatedSelectedOptionProducts = new Dictionary<int, CarModelOptionProduct>();

            var modelAvailableOptions = _productOptionRepository.GetProductOptionsByEAN(carModelEAN);

            foreach (var availableOption in modelAvailableOptions)
            {
                if (selectedOptionProducts[availableOption.Id] != null)
                {
                    var selectedEAN = selectedOptionProducts[availableOption.Id].EAN;
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

                    // If it is a default product, the product price is included in the car's base price
                    if (availableOption.DefaultProductIds.Contains(userSelectedProduct.Id))
                        continue;

                    optionsPrice += userSelectedProduct.NetPrice * (1 + userSelectedProduct.VATRate / 100);

                    validatedSelectedOptionProducts.Add(availableOption.Id, userSelectedOptionProduct);
                }
            }

            var totalPrice = basePrice + optionsPrice;

            var summary = new CarConfiguratorPriceSummary(basePrice, totalPrice, optionsPrice, validatedSelectedOptionProducts);

            return summary;
        }
    }
}