using System.Collections.Generic;

namespace CarConfigurator.BL.Models
{
    public class CarModelOption
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public IEnumerable<CarModelOptionProduct> Products { get; }

        public CarModelOption(int id, string name, string description, IEnumerable<CarModelOptionProduct> products = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Products = products;
        }
    }
}