using System.Collections.Generic;

namespace CarConfigurator.BL.Models
{
    public class CarModelOption
    {
        public string Name { get; }
        public string Description { get; }
        public IEnumerable<CarModelOptionProduct> Products { get; }

        public CarModelOption(string name, string description, IEnumerable<CarModelOptionProduct> products)
        {
            Name = name;
            Description = description;
            Products = products;
        }
    }
}