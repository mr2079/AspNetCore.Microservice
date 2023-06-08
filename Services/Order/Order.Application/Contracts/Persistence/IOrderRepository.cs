using Order.Appliction.Contracts.Persistence;
using OrderEntity =  Order.Domain.Entities.Order;

namespace Order.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<OrderEntity>
{
    Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName);
}
