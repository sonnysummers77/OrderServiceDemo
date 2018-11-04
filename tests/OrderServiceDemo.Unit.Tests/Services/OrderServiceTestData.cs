using OrderServiceDemo.Models;
using OrderServiceDemo.Core;
using System.Collections.Generic;

namespace OrderServiceDemo.Unit.Tests.Utility

{
    public class OrderServiceTestData
    {
        public static Order GetFakeOrder()
        {
            var order = new Order();
            order.OrderId = 1;
            order.OrderStatus = OrderStatus.GetOrderStatus(1001);
            order.OrderLineItems = GetFakeOrderLineItems();
            order.OrderStatusId = 1001;
            return order;
        }

        public static List<OrderLineItem> GetFakeOrderLineItems()
        {
            var orderLineitem1 = new OrderLineItem();
            var orderLineitem2 = new OrderLineItem();
            List<OrderLineItem> orderLineItems = new List<OrderLineItem>();
            orderLineItems.Add(GetFakeOrderLineItem(1,1));
            orderLineItems.Add(GetFakeOrderLineItem(1,2));
            return orderLineItems;
        }

        public static OrderLineItem GetFakeOrderLineItem(int orderId, int productId)
        {
            var orderLineItem = new OrderLineItem{
                OrderId = orderId,
                ProductId = productId,
                Quantity = 1,
                ItemPrice = 1.00M,
            };
            return orderLineItem;
        }
    }
}