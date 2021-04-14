using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConfigurator.DL.Models;

namespace CarConfigurator.BL.Models
{
    public class CarModel : IProduct
    {
        public string Name { get; }
        public string Description { get; }
        public int ProductId { get; set; }

        public CarModel(int productId, string name, string description)
        {
            ProductId = productId;
            Name = name;
            Description = description;
        }
    }
}