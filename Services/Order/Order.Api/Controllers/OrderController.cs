using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs;
using Order.Application.Features.Order.Commands;
using Order.Application.Features.Order.Queries;
using System.Net;

namespace Order.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{userName}", Name = "GetOrders")]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrdersByUserName(string userName)
    {
        var query = new GetOrdersListQuery(userName);
        var orders = await _mediator.Send(query);

        return Ok(orders);
    }


    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }


    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }


    [HttpDelete("{id}", Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await _mediator.Send(new DeleteOrderCommand() { Id = id });

        return NoContent();
    }
}
