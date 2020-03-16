using PizzaOrderApplication.Core.Entities;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderDetailsById(int orderId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
    }
}
