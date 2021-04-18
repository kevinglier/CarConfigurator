using System.Collections.Generic;
using CarConfigurator.DL.Models;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface IProductRepository : IRepository
    {
        public Product GetProduct(int id);

        /// <summary>
        /// Returns all main products (which doesn't belong to a product option)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetMainProducts();

        /// <summary>
        /// Returns a product with the specified EAN.
        /// </summary>
        /// <param name="ean"></param>
        /// <returns></returns>
        public Product GetByEAN(string ean);

        /// <summary>
        /// Returns the products which belong to a product option
        /// </summary>
        /// <param name="mainProduct"></param>
        /// <param name="productOption"></param>
        /// <returns></returns>
        IEnumerable<Product> GetOptionProducts(Product mainProduct, ProductOption productOption);

        /// <summary>
        /// Returns the products which belong to a product option
        /// </summary>
        /// <param name="mainProductId"></param>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        IEnumerable<Product> GetOptionProducts(int mainProductId, int productOptionId);
    }
}