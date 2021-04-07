namespace CarConfigurator.DL.Models
{
    public class ProductOption
    {
        public int Id { get; }
        public int ProductId { get; }
        public string Name { get; }
        public string Description { get; }

        public ProductOption(int id, int productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
        }
    }
}