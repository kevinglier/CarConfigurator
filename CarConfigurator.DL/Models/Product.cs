using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Models
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int? ProductOptionId { get; }

        public Product(int id, string name, string description, int? productOptionId)
        {
            Id = id;
            Name = name;
            Description = description;
            ProductOptionId = productOptionId;
        }

        public Product(string name, string description, int? productOptionId)
        {
            Name = name;
            Description = description;
            ProductOptionId = productOptionId;
        }
    }
}