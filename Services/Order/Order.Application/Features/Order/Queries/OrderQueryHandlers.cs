using AutoMapper;
using MediatR;
using Order.Application.Contracts.Persistence;
using Order.Application.DTOs;

namespace Order.Application.Features.Order.Queries;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrdersDto>> Handle(
        GetOrdersListQuery request,
        CancellationToken cancellationToken)
    {
        var ordersList = await _orderRepository.GetOrdersByUserName(request.UserName);
        
        return _mapper.Map<List<OrdersDto>>(ordersList);
    }
}
