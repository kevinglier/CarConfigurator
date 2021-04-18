namespace CarConfigurator.BL.Models
{
    public class CarModel : IProduct
    {
        public string Name { get; }
        public string Description { get; }
        public string EAN { get; set; }

        public CarModel(string ean, string name, string description)
        {
            EAN = ean;
            Name = name;
            Description = description;
        }
    }
}