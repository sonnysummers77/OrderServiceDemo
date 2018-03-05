using OrderServiceDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Infrastructure
{
    public interface IOrderLineItemRepository
    {
        Task<IEnumerable<OrderLineItem>> GetOrderLineItems(int orderId);
        Task<OrderLineItem> CreateOrderLineItem(OrderLineItem lineItem);
        Task<OrderLineItem> UpdateOrderLineItem(OrderLineItem lineItem);
        Task<OrderLineItem> DeleteOrderLineItem(OrderLineItem lineItem);
        Task<IEnumerable<OrderLineItem>> DeleteAllLineItemsInOrder(int orderId);
    }
}
