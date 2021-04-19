using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarConfigurator.DL.Models
{
    public class CarConfigUserConfiguration
    {
        [JsonIgnore]
        public int Id { get; }
        public string Code { get; }
        public string ModelEAN { get; }
        public IEnumerable<CarConfigUserConfigurationProduct> Products { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; }

        public CarConfigUserConfiguration(int id, string code, string modelEAN, DateTime createdAt = default)
        {
            Id = id;
            Code = code;
            ModelEAN = modelEAN;
            CreatedAt = createdAt;
        }

        public CarConfigUserConfiguration(string modelEAN, IEnumerable<CarConfigUserConfigurationProduct> products = null, DateTime createdAt = default)
        {
            ModelEAN = modelEAN;
            Products = products;
            CreatedAt = createdAt;
        }
    }
}