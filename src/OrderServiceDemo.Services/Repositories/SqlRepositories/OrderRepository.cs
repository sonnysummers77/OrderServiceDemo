using Dapper;
using Microsoft.Extensions.Options;
using OrderServiceDemo.Models;
using OrderServiceDemo.Models.Options;
using OrderServiceDemo.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Repositories.SqlRepositories
{
    public class OrderRepository : SqlRepository, IOrderRepository
    {
        public OrderRepository(IOptionsSnapshot<OrderServiceDemoOptions> options) 
            : base(options)
        {
        }

        public async Task<Order> CreateOrder(Order order)
        {
            using (var conn = await GetConnection())
            {
                const string insertOrderQuery = @"
                    Insert Into [Orders] (
                        [UserId], 
                        [OrderStatusId], 
                        [PurchasedOn]
                    )
                    Output Inserted.*
                    Values (
                        @UserId, 
                        @OrderStatusId, 
                        @PurchasedOn
                    )";

                var insertedOrder = conn.Query<Order>(insertOrderQuery, new
                {
                    order.UserId,
                    order.OrderStatusId,
                    order.PurchasedOn
                }).Single();

                return insertedOrder;
            }
        }

        public Task<Order> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrder(int orderId)
        {
            using (var conn = await GetConnection())
            {
                const string query = @"Select * From [Orders] Where [OrderId] = @OrderId";
                var order = conn.Query<Order>(query, new { orderId }).Single();
                return order;
            }
        }

        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            using (var conn = await GetConnection())
            {
                const string query = @"Select * From [Orders] Where [UserId] = @UserId";
                var orders = conn.Query<Order>(query, new { userId }).ToList();
                return orders;
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            using (var conn = await GetConnection())
            {
                const string orderQuery = @"
                    Update [Orders] 
                    Set 
                        [UserId] = @UserId, 
                        [OrderStatusId] = @OrderStatusId, 
                        [PurchasedOn] = @PurchasedOn
                    Output Inserted.*
                    Where [OrderId] = @OrderId";

                var updatedOrder = conn.Query<Order>(orderQuery, new
                {
                    order.OrderId,
                    order.UserId,
                    order.OrderStatusId,
                    order.PurchasedOn
                }).Single();

                return updatedOrder;
            }
        }
    }
}
