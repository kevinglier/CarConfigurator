using System.Collections.Generic;

namespace CarConfigurator.DL.Models
{
    public class ProductOption
    {
        public IEnumerable<int> DefaultProductIds;
        public int Id { get; }
        public string EAN { get; }
        public int ProductId { get; }
        public string Name { get; }
        public string Description { get; }

        public ProductOption()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ean">EAN des Haupt-Produktes</param>
        /// <param name="productId">ID des Haupt-Produktes</param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="defaultProductIds"></param>
        public ProductOption(int id, string ean, int productId, string name, string description, IEnumerable<int> defaultProductIds)
        {
            Id = id;
            EAN = ean;
            ProductId = productId;
            Name = name;
            Description = description;
            DefaultProductIds = defaultProductIds;
        }
    }
}