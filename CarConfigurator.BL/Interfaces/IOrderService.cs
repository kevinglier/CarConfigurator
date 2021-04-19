using CarConfigurator.BL.Models;
using CarConfigurator.DL.Models;

namespace CarConfigurator.BL.Interfaces
{
    public interface IOrderService
    {
        CarOrderDetails AddOrder(CarOrderDetails carOrderDetails);
    }
}