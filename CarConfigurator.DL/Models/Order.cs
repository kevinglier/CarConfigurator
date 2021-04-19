using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConfigurator.DL.Models
{
    public class Order
    {
        public int Id { get; }
        public string Code { get; }
        public DateTime CreatedAt { get; }

        private readonly List<OrderPosition> _positions = new();

        public Order(string code)
        {
            Code = code;
        }

        public Order(int id, string code, DateTime createdAt)
        {
            Id = id;
            Code = code;
            CreatedAt = createdAt;
        }

        public IEnumerable<OrderPosition> Positions => _positions;

        public void AddPosition(OrderPosition position)
        {
            _positions.Add(position);
        }
    }
}