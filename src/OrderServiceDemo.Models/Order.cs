using OrderServiceDemo.Core;
using System;
using System.Collections.Generic;

namespace OrderServiceDemo.Models
{
    public class Order
    {
        private int _orderStatusId;
        private OrderStatus _orderStatus;

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId
        {
            get { return _orderStatusId; }
            set
            {
                _orderStatusId = value;
                _orderStatus = OrderStatus.GetOrderStatus(value);
            }
        }
        public DateTime PurchasedOn { get; set; }
        public OrderStatus OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                _orderStatus = value;
                _orderStatusId = value.ID;
            }
        }
        public List<OrderLineItem> OrderLineItems { get; set; }
    }
}
