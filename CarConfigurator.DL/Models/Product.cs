using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Models
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int ManufacturerId { get; }
        public bool IsOptionProduct { get; }

        public Product(int id, string name, string description, int manufacturerId, bool isOptionProduct)
        {
            Id = id;
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId;
            IsOptionProduct = isOptionProduct;
        }

        public Product(string name, string description, int manufacturerId, bool isOptionProduct)
        {
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId;
            IsOptionProduct = isOptionProduct;
        }
    }
}