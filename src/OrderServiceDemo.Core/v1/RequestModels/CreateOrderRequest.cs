using System.Collections.Generic;

namespace OrderServiceDemo.Core.v1.RequestModels
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public IEnumerable<OrderLineItem> OrderLineItems { get; set; }
    }
}
