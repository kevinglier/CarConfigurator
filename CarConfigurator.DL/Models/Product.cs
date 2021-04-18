using CarConfigurator.DL.Repositories.Interfaces;

namespace CarConfigurator.DL.Models
{
    public class Product
    {
        public int Id { get; }
        public string EAN { get; }
        public string Name { get; }
        public string Description { get; }
        public int ManufacturerId { get; }
        public bool IsOptionProduct { get; }

        public Product()
        {
            
        }

        public Product(int id, string ean, string name, string description, int manufacturerId, bool isOptionProduct)
        {
            Id = id;
            EAN = ean;
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