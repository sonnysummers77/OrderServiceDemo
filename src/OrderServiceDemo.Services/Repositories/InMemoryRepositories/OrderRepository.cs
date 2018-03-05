using OrderServiceDemo.Models;
using OrderServiceDemo.Models.Exceptions;
using OrderServiceDemo.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Repositories.InMemoryRepositories
{
    public class OrderRepository : InMemoryRepository<Order>, IOrderRepository
    {
        private readonly IOrderLineItemRepository _orderLineItemRepository;

        public OrderRepository(IOrderLineItemRepository orderLineItemRepository)
        {
            _orderLineItemRepository = orderLineItemRepository;
        }

        protected override Action<Order> SetIdentity => ((x) => x.OrderId = Entities.Count() + 1);

        public Task<Order> CreateOrder(Order order)
        {
            return AddEntity(order);
        }

        public async Task<Order> DeleteOrder(Order order)
        {
            var lineItems = await _orderLineItemRepository.GetOrderLineItems(order.OrderId);
            if (lineItems?.Any() == true)
                throw new InMemoryRepositoryException($"Simulated Foreign Key Constraint - Line Items exist for order {order.OrderId}");

            return await DeleteEntity(order);
        }

        public Task<Order> GetOrder(int orderId)
        {
            var order = Entities.SingleOrDefault(x => x.OrderId == orderId);
            return Task.FromResult(order);
        }

        public Task<IEnumerable<Order>> GetOrders(int userId)
        {
            var orders = Entities.Where(x => x.UserId == userId);
            return Task.FromResult(orders);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var existing = Entities.SingleOrDefault(x => x.OrderId == order.OrderId);
            if (existing == null)
                return null; //This would most closely match the behavior of an update sql script (with output inserted.*) that updated nothing.

            await DeleteEntity(existing);
            var result = await AddEntity(order);
            return result;
        }
    }
}
