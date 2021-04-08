using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConfigurator.BL.Models
{
    public class CarModel
    {
        public string Name { get; }
        public string Description { get; }

        public CarModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
