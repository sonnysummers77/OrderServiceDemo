namespace OrderServiceDemo.Core.v1
{
    public class OrderLineItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
