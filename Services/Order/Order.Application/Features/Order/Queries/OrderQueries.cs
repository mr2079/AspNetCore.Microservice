using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Features.Order.Queries;

public class GetOrdersListQuery : IRequest<List<OrdersDto>>
{
    public GetOrdersListQuery(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}
