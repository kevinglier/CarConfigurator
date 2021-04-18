using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Base;
using CarConfigurator.DL.Repositories.Interfaces;
using Dapper;

namespace CarConfigurator.DL.Repositories
{
    public class ProductOptionRepository : RepositoryBase, IProductOptionRepository
    {
        public ProductOptionRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<ProductOption> GetProductOptions(int productId)
        {
            const string sql = @"
                SELECT *
                  FROM ProductOption
                 WHERE Id IN (
                    SELECT po.Id
	                  FROM ProductOption_Product po_p
	                  JOIN ProductOption po ON po.Id = po_p.ProductOptionId
	                 WHERE po_p.ProductId = @productId )
                ";

            using var connection = new SqlConnection(ConnectionString);
            var productOptions = connection.Query<ProductOption>(sql, new { productId });

            return productOptions;
        }

        public IEnumerable<ProductOption> GetProductOptionsByEAN(string ean)
        {
            const string sql = @"
                SELECT *
                  FROM ProductOption
                 WHERE Id IN (
                    SELECT po.Id
	                  FROM ProductOption_Product po_p
	                  JOIN ProductOption po ON po.Id = po_p.ProductOptionId
	                 WHERE po_p.ProductId = (SELECT Id FROM Product WHERE EAN=@ean) )
                ";

            using var connection = new SqlConnection(ConnectionString);
            var productOptions = connection.Query<ProductOption>(sql, new { ean });

            var mainProductId = connection.ExecuteScalar<int>("SELECT Id FROM Product WHERE EAN=@ean", new {ean});

            productOptions = productOptions.Select(productOption =>
            {

                productOption.DefaultProductIds = GetDefaultProducts(productOption, mainProductId);

                return productOption;
            });

            return productOptions;
        }

        private IEnumerable<int> GetDefaultProducts(ProductOption productOption, int mainProductId)
        {
            const string sql = @"
                SELECT ProductOption_ProductId
                  FROM ProductOption_Product po_p
                 WHERE po_p.ProductId = @mainProductId
                   AND po_p.ProductOptionId = @productOptionId
                   AND IsDefault=1
                ";

            using var connection = new SqlConnection(ConnectionString);
            var defaultProductIds = connection.Query<int>(sql, new { productOptionId = productOption.Id, mainProductId });
            
            return defaultProductIds;
        }
    }
}