using OrderServiceDemo.Models;
using OrderServiceDemo.Services.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Repositories.InMemoryRepositories
{
    public class OrderLineItemRepository : InMemoryRepository<OrderLineItem>, IOrderLineItemRepository
    {
        public Task<OrderLineItem> CreateOrderLineItem(OrderLineItem lineItem)
        {
            return AddEntity(lineItem);
        }
        
        public Task<IEnumerable<OrderLineItem>> DeleteAllLineItemsInOrder(int orderId)
        {
            return DeleteEntities(x => x.OrderId == orderId);
        }

        public Task<OrderLineItem> DeleteOrderLineItem(OrderLineItem lineItem)
        {
            return DeleteEntity(lineItem);
        }

        public Task<IEnumerable<OrderLineItem>> GetOrderLineItems(int orderId)
        {
            return GetEntities(x => x.OrderId == orderId);
        }

        public async Task<OrderLineItem> UpdateOrderLineItem(OrderLineItem lineItem)
        {
            var match = await GetEntity(x => x.OrderId == lineItem.OrderId && x.ProductId == lineItem.ProductId);
            if (match == null)
                return null;

            await DeleteEntity(match);
            var result = await AddEntity(lineItem);
            return result;
        }
    }
}
