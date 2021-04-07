using System.Collections.Generic;
using System.Data.SqlClient;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Base;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public Product GetProduct(int id)
        {
            return new Product(ConnectionString, "", "", null);
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