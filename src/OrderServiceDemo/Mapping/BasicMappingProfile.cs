using AutoMapper;

namespace OrderServiceDemo.Mapping
{
    public class BasicMappingProfile<TSource, TDestination> : Profile
    {
        public BasicMappingProfile()
        {
            CreateMap<TSource, TDestination>().ReverseMap();
        }
    }
}
