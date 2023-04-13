using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entity;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderVm>()
            .ReverseMap();

        CreateMap<Order, CheckoutOrderCommand>()
            .ReverseMap();

        CreateMap<Order, UpdateOrderCommand>()
            .ReverseMap();
    }
}