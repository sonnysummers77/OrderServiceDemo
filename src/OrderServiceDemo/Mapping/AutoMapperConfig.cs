using AutoMapper;

namespace OrderServiceDemo.Mapping
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BasicMappingProfile<Models.Order, Core.v1.Order>());
            });
        }
    }
}
