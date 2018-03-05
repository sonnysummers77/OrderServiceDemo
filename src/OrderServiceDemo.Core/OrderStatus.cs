using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace OrderServiceDemo.Core
{
    public sealed class OrderStatus : EnumClass
    {
        public static readonly OrderStatus Open = new OrderStatus(1001, "Open");
        public static readonly OrderStatus Pending = new OrderStatus(1009, "Pending");
        public static readonly OrderStatus Cancelled = new OrderStatus(1025, "Cancelled");

        [JsonConstructor]
        private OrderStatus(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public static OrderStatus GetOrderStatus(int id)
        {
            return GetOrderStatuses().SingleOrDefault(x => x.ID == id);
        }

        public static IEnumerable<OrderStatus> GetOrderStatuses()
        {
            return new[]
            {
                Open,
                Pending,
                Cancelled
            };
        }
    }
}
