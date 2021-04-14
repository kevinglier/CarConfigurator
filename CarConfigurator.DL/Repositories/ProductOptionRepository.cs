using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}