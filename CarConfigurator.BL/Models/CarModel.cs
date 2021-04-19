namespace CarConfigurator.BL.Models
{
    public class CarModel : IProduct
    {
        public string Name { get; }
        public string Description { get; }
        public decimal BasePrice { get; }
        public string EAN { get; set; }

        public CarModel(string ean, string name, string description, decimal basePrice)
        {
            EAN = ean;
            Name = name;
            Description = description;
            BasePrice = basePrice;
        }
    }
}