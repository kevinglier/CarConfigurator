using System.Collections.Generic;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(SqlConnection sqlConnection)
        {
        }

        public Product GetProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(ProductOption productOption)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetMainProducts()
        {
            throw new System.NotImplementedException();
        }
    }
}