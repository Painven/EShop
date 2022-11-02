using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;

namespace EShopAPI.Mapper;

public class OrderAutomapperProfile : Profile
{
    public OrderAutomapperProfile()
    {
        CreateMap<OrderLine, OrderLineDto>();

        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>()
            .ForMember(o => o.TotalSum, o => o.MapFrom(x => x.OrderLines.Sum(ol => ol.Price)));

    }
}
