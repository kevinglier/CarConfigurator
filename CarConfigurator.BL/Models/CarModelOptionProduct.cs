namespace CarConfigurator.BL.Models
{
    public class CarModelOptionProduct : IProduct
    {
        public string Name { get; }
        public string Description { get; }

        public CarModelOptionProduct(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}