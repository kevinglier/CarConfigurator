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
        public decimal NetPrice { get; set; }
        public decimal VATRate { get; }

        public Product()
        {
        }

        public Product(int id, string ean, string name, string description, int manufacturerId, decimal netPrice, decimal vatRate, bool isOptionProduct)
        {
            Id = id;
            EAN = ean;
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId;
            NetPrice = netPrice;
            VATRate = vatRate;
            IsOptionProduct = isOptionProduct;
        }

        public Product(string name, string description, int manufacturerId, decimal netPrice, decimal vatRate, bool isOptionProduct)
        {
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId;
            NetPrice = netPrice;
            VATRate = vatRate;
            IsOptionProduct = isOptionProduct;
        }
    }
}