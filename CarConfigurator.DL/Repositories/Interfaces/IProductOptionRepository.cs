using System.Collections.Generic;
using CarConfigurator.DL.Models;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface IProductOptionRepository : IRepository
    {
        public IEnumerable<ProductOption> GetProductOptions(int productId);
        public IEnumerable<ProductOption> GetProductOptionsByEAN(string ean);
        public ProductOption GetById(int id, int mainProductId);
    }
}