using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public Product GetById(int id)
        {
            const string sql = "SELECT * FROM Product WHERE Id = @id;";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { id });
                
            return product;
        }

        public Product GetByEAN(string ean)
        {
            const string sql = "SELECT * FROM Product WHERE EAN=@ean";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { ean });

            return product;
        }

        public Product GetByName(string name)
        {
            const string sql = "SELECT * FROM Product WHERE Name LIKE @name";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { name });

            return product;
        }

        public IEnumerable<Product> GetOptionProducts(Product mainProduct, ProductOption productOption)
        {
            return this.GetOptionProducts(mainProduct.Id, productOption.Id);
        }

        public IEnumerable<Product> GetOptionProducts(int mainProductId, int productOptionId)
        {
            const string sql = @"
                SELECT *
                  FROM Product
                 WHERE Id IN (
                    SELECT po_p.ProductOption_ProductId
	                  FROM ProductOption_Product po_p
	                  JOIN ProductOption po ON po.Id = po_p.ProductOptionId
	                 WHERE po.Id = @productOptionId AND po_p.ProductId = @mainProductId )";

            using var connection = new SqlConnection(ConnectionString);
            var products = connection.Query<Product>(sql, new { productOptionId, mainProductId });

            return products;
        }

        public IEnumerable<Product> GetProductsByIds(IEnumerable<int> ids)
        {
            const string sql = @"
                SELECT *
                  FROM Product
                 WHERE Id IN (@ids)";

            using var connection = new SqlConnection(ConnectionString);
            var products = connection.Query<Product>(sql, new { ids });

            return products;
        }

        public IEnumerable<Product> GetMainProducts()
        {
            const string sql = "SELECT * FROM Product WHERE IsOptionProduct=0";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.Query<Product>(sql);

            return product;
        }
    }
}