using System.Collections;
using System.Collections.Generic;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Base;
using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Repositories
{
    public class ProductOptionRepository : RepositoryBase, IProductOptionRepository
    {
        public ProductOptionRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<ProductOption> GetProductOptions(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}