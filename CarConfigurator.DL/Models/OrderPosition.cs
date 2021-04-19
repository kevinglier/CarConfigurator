using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConfigurator.DL.Models
{
    public class OrderPosition
    {
        public int Id { get; }
        public int OrderId { get; }
        public string EAN { get; }
        public decimal NetPrice { get; }
        public decimal VATRate { get; }
        public string Name { get; }

        public OrderPosition(string ean, decimal netPrice, decimal vatRate, string name)
        {
            EAN = ean;
            NetPrice = netPrice;
            VATRate = vatRate;
            Name = name;
        }

        public OrderPosition(int id, int orderId, string ean, decimal netPrice, decimal vatRate, string name)
        {
            Id = id;
            OrderId = orderId;
            EAN = ean;
            NetPrice = netPrice;
            VATRate = vatRate;
            Name = name;
        }
    }
}
