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
            const string sql = "SELECT * FROM Product WHERE Id = @id;";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { id });
                
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
            var product = connection.Query<Product>(sql, new { productOptionId, mainProductId });

            return product;
        }

        public IEnumerable<Product> GetMainProducts()
        {
            const string sql = "SELECT * FROM Product WHERE IsOptionProduct=0";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.Query<Product>(sql);

            return product;
        }

        public Product GetByEAN(string ean)
        {
            const string sql = "SELECT * FROM Product WHERE EAN LIKE @ean";

            using var connection = new SqlConnection(ConnectionString);
            var product = connection.QuerySingleOrDefault<Product>(sql, new { ean});

            return product;
        }
    }
}