using System.Collections.Generic;

namespace CarConfigurator.BL.Models
{
    public class CarOrderDetails
    {
        public string CarModelEAN { get; }
        public IDictionary<int, CarModelOptionProduct> CarOptionProducts { get; }
        public string OrderNo { get; set; }
        public string Code { get; set; }

        public CarOrderDetails(
            string carModelEAN,
            IDictionary<int, CarModelOptionProduct> carOptionProducts,
            string code,
            string orderNo = null)
        {
            CarModelEAN = carModelEAN;
            CarOptionProducts = carOptionProducts;
            Code = code;
            OrderNo = orderNo;
        }
    }
}