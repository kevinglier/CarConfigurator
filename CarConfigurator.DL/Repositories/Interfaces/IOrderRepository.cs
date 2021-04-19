using CarConfigurator.DL.Models;

namespace CarConfigurator.DL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order);
        Order GetOrder(int orderId);
    }
}