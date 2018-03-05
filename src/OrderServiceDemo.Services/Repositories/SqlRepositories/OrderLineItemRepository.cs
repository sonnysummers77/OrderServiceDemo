using Dapper;
using Microsoft.Extensions.Options;
using OrderServiceDemo.Models;
using OrderServiceDemo.Models.Options;
using OrderServiceDemo.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Repositories.SqlRepositories
{
    public class OrderLineItemRepository : SqlRepository, IOrderLineItemRepository
    {
        public OrderLineItemRepository(IOptionsSnapshot<OrderServiceDemoOptions> options) 
            : base(options)
        {
        }

        public async Task<OrderLineItem> CreateOrderLineItem(OrderLineItem lineItem)
        {
            using (var conn = await GetConnection())
            {
                const string lineItemQuery = @"
                    Insert Into [OrderLineItems] (
                        [OrderId], 
                        [ProductId], 
                        [Quantity], 
                        [ItemPrice]
                    )
                    Output Inserted.* 
                    Values (
                        @OrderId, 
                        @ProductId, 
                        @Quantity, 
                        @ItemPrice
                    )";

                var insertedItem = conn.Query<OrderLineItem>(lineItemQuery, lineItem).Single();

                return insertedItem;
            }
        }

        public Task<IEnumerable<OrderLineItem>> DeleteAllLineItemsInOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderLineItem> DeleteOrderLineItem(OrderLineItem lineItem)
        {
            using (var conn = await GetConnection())
            {
                const string deleteLineItems = @"
                    Delete From [OrderLineItems] 
                    Output Deleted.* 
                    Where 
                        [OrderID] = @OrderId
                    and [ProductID] = @ProductId";

                var deletedLineItem = conn.Query<OrderLineItem>(deleteLineItems, new
                {
                    lineItem.OrderId,
                    lineItem.ProductId
                }).Single();
                return deletedLineItem;
            }
        }

        public async Task<IEnumerable<OrderLineItem>> GetOrderLineItems(int orderId)
        {
            using (var conn = await GetConnection())
            {
                const string query = @"Select * From [OrderLineItems] Where [OrderId] = @orderId";
                var lineItems = conn.Query<OrderLineItem>(query, new { orderId }).ToList();
                return lineItems;
            }
        }

        public async Task<OrderLineItem> UpdateOrderLineItem(OrderLineItem lineItem)
        {
            using (var conn = await GetConnection())
            {
                const string lineItemQuery = @"
                    Update [OrderLineItems] 
                    Set 
                        [Quantity] = @Quantity, 
                        [ItemPrice] = @ItemPrice
                    Output Inserted.* 
                    Where 
                        [OrderId] = @OrderId 
                    And [ProductId] = @ProductId";

                var updatedItem = conn.Query<OrderLineItem>(lineItemQuery, lineItem).Single();
                return updatedItem;
            }
        }
    }
}
