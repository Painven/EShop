using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;

namespace EShopAPI.Mapper;

public class OrderAutomapperProfile : Profile
{
    public OrderAutomapperProfile()
    {      
        CreateMap<CreateOrderDto, Order>();
        CreateMap<Order, CreateOrderDto>();
        
        
        CreateMap<OrderLine, OrderLineDto>();
        CreateMap<OrderLineDto, OrderLine>();

        CreateMap<OrderDto, Order>()
            .ForMember(o => o.OrderLines, o => o.MapFrom(x => x.Products));

        CreateMap<Order, OrderDto>()
            .ForMember(o => o.OrderStatus, o => o.MapFrom(x => x.OrderStatus.Name))
            .ForMember(o => o.TotalSum, o => o.MapFrom(x => x.OrderLines.Sum(ol => ol.Price * ol.Quantity)))
            .ForMember(o => o.Products, o => o.MapFrom(x => x.OrderLines));

    }
}
