using System.Collections.Generic;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface IProductOptionRepository : IRepository
    {
        public IEnumerable<ProductOption> GetProductOptions(Product product);
    }
}