using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Base;
using CarConfigurator.DL.Repositories.Interfaces;
using Dapper;

namespace CarConfigurator.DL.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public Order AddOrder(Order order)
        {
            using var connection = new SqlConnection(ConnectionString);

            var orderId = connection.ExecuteScalar<int>(@"INSERT INTO [Order] (Code) OUTPUT INSERTED.ID VALUES (@code);", new { order.Code });

            foreach (var position in order.Positions)
            {
                connection.Execute(@"INSERT INTO OrderPosition (OrderId, EAN, NetPrice, VATRate, Name) VALUES (@orderId, @ean, @netPrice, @vatRate, @name);", 
                    new { orderId, position.EAN, position.NetPrice, position.VATRate, position.Name });
            }

            return GetOrder(orderId);
        }

        public Order GetOrder(int id)
        {
            const string sql = "SELECT * FROM [Order] WHERE Id=@id";

            using var connection = new SqlConnection(ConnectionString);
            var order = connection.QuerySingleOrDefault<Order>(sql, new { id });

            if (order == null)
                return null;

            var orderPositions = GetOrderPositions(order.Id);
            foreach (var orderPosition in orderPositions)
            {
                order.AddPosition(orderPosition);
            }

            return order;
        }

        public IEnumerable<OrderPosition> GetOrderPositions(int orderId)
        {
            const string sql = "SELECT * FROM OrderPosition WHERE OrderId=@orderId";

            using var connection = new SqlConnection(ConnectionString);
            return connection.Query<OrderPosition>(sql, new { orderId });
        }
    }
}