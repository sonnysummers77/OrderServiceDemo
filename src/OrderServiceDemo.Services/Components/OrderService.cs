using System.Linq;
using System.Threading.Tasks;
using OrderServiceDemo.Core;
using OrderServiceDemo.Models;
using OrderServiceDemo.Models.Exceptions;
using OrderServiceDemo.Services.Infrastructure;
using OrderServiceDemo.Services.Interfaces;

namespace OrderServiceDemo.Services.Components
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineItemRepository _orderLineItemRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderLineItemRepository orderLineItemRepository)
        {
            _orderRepository = orderRepository;
            _orderLineItemRepository = orderLineItemRepository;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            if (order.OrderLineItems?.Any() != true)
                throw new InvalidRequestException("To create an order you must supply at least 1 line item");

            var createdOrder = await _orderRepository.CreateOrder(order);

            foreach(var lineItem in order.OrderLineItems)
            {
                lineItem.OrderId = createdOrder.OrderId;
            }

            var lineItems = await Task.WhenAll(order.OrderLineItems.Select(x => _orderLineItemRepository.CreateOrderLineItem(x)));
            createdOrder.OrderLineItems = lineItems.ToList();
            return createdOrder;
        }

        public async Task<Order> GetOrder(int orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);
            await BuildUpOrder(order);
            return order;
        }

        public async Task<Order> CancelOrder(int orderId)
        {
            var order = await GetOrder(orderId);

            if(order != null)
            {
                order.OrderStatus = OrderStatus.GetOrderStatus(1025);
                var result = await _orderRepository.UpdateOrder(order);

                if (result != null)
                    return result;
                else
                    throw new OrderIsAlreadyCanceledException("Requested order is already canceled");
            }
            else
                throw new OrderDoesNotExistException("Order cannot be cancelled because it doesn't exist");
            
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
           
            var order = await GetOrder(orderId);
            if (order != null)
            {
                await _orderLineItemRepository.DeleteAllLineItemsInOrder(orderId);
                await _orderRepository.DeleteOrder(order);
            }
            else
                throw new OrderDoesNotExistException("Order was not deleted. It did not exist.");
   
            return order;
        }

        private async Task<Order> BuildUpOrder(Order order)
        {
            if (order == null)
                return order;

            var lineItems = await _orderLineItemRepository.GetOrderLineItems(order.OrderId);
            order.OrderLineItems = lineItems.ToList();
            return order;
        }
    }
}
