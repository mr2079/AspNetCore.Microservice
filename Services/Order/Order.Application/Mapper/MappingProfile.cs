using AutoMapper;
using Order.Application.DTOs;
using Order.Application.Features.Order.Commands;
using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderEntity, OrdersDto>().ReverseMap();
        CreateMap<OrderEntity, CheckoutOrderCommand>().ReverseMap();
        CreateMap<OrderEntity, UpdateOrderCommand>().ReverseMap();
    }
}
