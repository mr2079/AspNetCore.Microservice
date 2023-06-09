using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Infrastructure.Persistence;
using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Infrastructure.Implementation.Repositories;

public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
{
    public OrderRepository(OrderContext context) : base(context) { }

    public async Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName)
        => await _context.Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
}
