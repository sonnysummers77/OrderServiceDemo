using OrderServiceDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Infrastructure
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(int orderId);
        Task<IEnumerable<Order>> GetOrders(int userId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> DeleteOrder(Order order);
    }
}
