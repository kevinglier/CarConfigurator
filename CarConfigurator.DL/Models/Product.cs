using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Models
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string ProductImage { get; }
        public int? OptionId { get; }

        public Product(int id, string name, string description, string productImage, int? optionId)
        {
            Id = id;
            Name = name;
            Description = description;
            ProductImage = productImage;
            OptionId = optionId;
        }

        public Product(string name, string description, string productImage, int? optionId)
        {
            Name = name;
            Description = description;
            ProductImage = productImage;
            OptionId = optionId;
        }
    }
}