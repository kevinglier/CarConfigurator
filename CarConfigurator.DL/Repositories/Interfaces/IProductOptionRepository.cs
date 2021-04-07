using System.Collections.Generic;
using CarConfigurator.DL.Models;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface IProductOptionRepository : IRepository
    {
        public IEnumerable<ProductOption> GetProductOptions(Product product);
    }
}