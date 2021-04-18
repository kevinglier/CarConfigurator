namespace CarConfigurator.DL.Models
{
    public class ProductOption
    {
        public int Id { get; }
        public string EAN { get; }
        public int ProductId { get; }
        public string Name { get; }
        public string Description { get; }

        public ProductOption()
        {
            
        }

        public ProductOption(int id, string ean, int productId, string name, string description)
        {
            Id = id;
            EAN = ean;
            ProductId = productId;
            Name = name;
            Description = description;
        }
    }
}