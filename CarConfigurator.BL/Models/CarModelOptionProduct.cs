namespace CarConfigurator.BL.Models
{
    public class CarModelOptionProduct : IProduct
    {
        public string EAN { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public bool IsDefault { get; }

        public CarModelOptionProduct(string ean, string name, string description, decimal price, bool isDefault)
        {
            EAN = ean;
            Name = name;
            Description = description;
            Price = price;
            IsDefault = isDefault;
        }
    }
}