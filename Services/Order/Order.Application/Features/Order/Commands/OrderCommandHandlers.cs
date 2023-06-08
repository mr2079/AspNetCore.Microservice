using AutoMapper;
using MediatR;
using Order.Application.Contracts.Persistence;
using Order.Application.Exceptions;
using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Application.Features.Order.Commands;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<OrderEntity>(request);
        var newOrder = await _orderRepository.CreateAsync(orderEntity);

        return newOrder.Id;
    }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);

        if (orderForUpdate == null)
            throw new NotFoundException(nameof(OrderEntity), request.Id);

        _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(OrderEntity));

        await _orderRepository.UpdateAsync(orderForUpdate);
    }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderForDelete = await _orderRepository.GetByIdAsync(request.Id);

        if (orderForDelete == null)
            throw new NotFoundException(nameof(OrderEntity), request.Id);

        await _orderRepository.DeleteAsync(orderForDelete);
    }
}
