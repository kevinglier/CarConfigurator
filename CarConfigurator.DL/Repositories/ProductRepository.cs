using System.Collections.Generic;
using System.Data.SqlClient;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Base;
using CarConfigurator.DL.Repositories.Interfaces;
using Dapper;

namespace CarConfigurator.DL.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public Product GetProduct(int id)
        {
            const string sql = "SELECT * FROM Product WHERE Id = @Id;";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
                
            return product;
        }

        public IEnumerable<Product> GetProducts(ProductOption productOption)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetMainProducts()
        {
            const string sql = "SELECT * FROM Product WHERE IsOptionProduct=0";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.Query<Product>(sql);

            return product;
        }

        public Product GetByName(string name)
        {
            const string sql = "SELECT * FROM Product WHERE Name LIKE @Name";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { Name = name});

            return product;
        }
    }
}