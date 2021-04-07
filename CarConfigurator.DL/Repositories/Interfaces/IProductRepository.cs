using System.Collections.Generic;
using CarConfigurator.DL.Models;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface IProductRepository : IRepository
    {
        public Product GetProduct(int id);

        /// <summary>
        /// Returns the productOption which belong to a ProductOption
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts(ProductOption productOption);

        /// <summary>
        /// Returns all main products (which doesn't belong to a product option)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetMainProducts();
    }
}