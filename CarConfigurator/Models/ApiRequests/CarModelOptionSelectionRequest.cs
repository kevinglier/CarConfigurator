using System.Collections.Generic;
using CarConfigurator.BL.Models;

namespace CarConfigurator.Models.ApiRequests
{
    public class CarModelOptionSelectionRequest
    {
        public string Code { get; set; }

        public string CarModelEAN { get; set; }

        public Dictionary<int, CarModelOptionProduct> OptionProducts { get; set; }
    }
}