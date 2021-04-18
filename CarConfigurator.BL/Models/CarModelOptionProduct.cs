namespace CarConfigurator.BL.Models
{
    public class CarModelOptionProduct : IProduct
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public CarModelOptionProduct(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}