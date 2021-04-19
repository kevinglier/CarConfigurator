using System;
using System.Collections.Generic;
using System.Linq;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(
            IProductRepository productRepository,
            IProductOptionRepository productOptionRepository,
            IOrderRepository orderRepository
        )
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
            _orderRepository = orderRepository;
        }

        public CarOrderDetails AddOrder(CarOrderDetails carOrderDetails)
        {
            var carModelProduct = _productRepository.GetByEAN(carOrderDetails.CarModelEAN);
            if (carModelProduct == null)
                throw new Exception("The product is unknown.");

            var order = new Order(carOrderDetails.Code);
            order.AddPosition(new OrderPosition(
                carModelProduct.EAN,
                carModelProduct.NetPrice,
                carModelProduct.VATRate,
                "Basis-Modell: " + carModelProduct.Name));

            foreach (var (optionId, carModelOptionProduct) in carOrderDetails.CarOptionProducts)
            {
                var option = _productOptionRepository.GetById(optionId, carModelProduct.Id);
                if (option == null)
                    throw new Exception("The option with ID " + optionId + " is unknown.");

                var optionProduct = _productRepository.GetByEAN(carModelOptionProduct.EAN);
                if (optionProduct == null)
                    throw new Exception("The product with EAN " + carModelOptionProduct.EAN + " is unknown.");
                
                order.AddPosition(new OrderPosition(
                    optionProduct.EAN, 
                    optionProduct.NetPrice,
                    optionProduct.VATRate,
                    "Option " + option.Name + ": " + optionProduct.Name));
            }

            order = _orderRepository.AddOrder(order);

            carOrderDetails.OrderNo = order.Id.ToString();

            return carOrderDetails;
        }
    }
}