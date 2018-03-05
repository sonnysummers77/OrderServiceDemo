namespace OrderServiceDemo.Models.Options
{
    public class OrderServiceDemoOptions
    {
        public string Environment { get; set; }
        public string OrderDBConnection { get; set; }
        public string RedisCacheConnection { get; set; }
        public int RedisCachePort { get; set; }
        public int RedisCacheDatabase { get; set; }
        public string MQApiBaseUrl { get; set; }
        public string MQAppId { get; set; }
        public string MQMessageExchange { get; set; }
    }
}
