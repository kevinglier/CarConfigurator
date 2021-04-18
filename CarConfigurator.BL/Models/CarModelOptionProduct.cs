namespace CarConfigurator.BL.Models
{
    public class CarModelOptionProduct : IProduct
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsDefault { get; }

        public CarModelOptionProduct(int id, string name, string description, bool isDefault)
        {
            Id = id;
            Name = name;
            Description = description;
            IsDefault = isDefault;
        }
    }
}