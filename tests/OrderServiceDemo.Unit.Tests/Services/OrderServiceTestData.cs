using OrderServiceDemo.Models;
using OrderServiceDemo.Core;
using System.Collections.Generic;

namespace OrderServiceDemo.Unit.Tests.Services
{
    public class OrderServiceTestData
    {
        public static Order GetFakeOrder()
        {
            var orderLineitem = new OrderLineItem();
            orderLineitem.OrderId= 1;
            orderLineitem.ItemPrice = 1;
            orderLineitem.ProductId= 1;
            orderLineitem.Quantity = 1;
            var order = new Order();
            order.OrderId = 1;
            order.OrderStatus = OrderStatus.GetOrderStatus(1001);
            order.OrderLineItems = new System.Collections.Generic.List<OrderLineItem>(); 
            order.OrderLineItems.Add(orderLineitem);
            order.OrderStatusId = 1001;
            return order;
        }

        public static List<OrderLineItem> GetFakeOrderLineItems()
        {
            var orderLineitem1 = new Models.OrderLineItem();
            var orderLineitem2 = new Models.OrderLineItem();
            List<OrderLineItem> orderLineItems = new List<OrderLineItem>();
            orderLineItems.Add(orderLineitem1);
            orderLineItems.Add(orderLineitem2);
            return orderLineItems;
        }
    }
}