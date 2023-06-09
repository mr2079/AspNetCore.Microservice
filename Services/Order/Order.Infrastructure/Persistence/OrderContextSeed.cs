using Microsoft.EntityFrameworkCore;
using OrderEntity = Order.Domain.Entities.Order;

namespace Order.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(GetPreconfiguredOrders());
            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<OrderEntity> GetPreconfiguredOrders()
        => new List<OrderEntity>()
        {
            new OrderEntity
            {
               FirstName = "first1",
               LastName = "last1",
               UserName = "user1",
               City = "city",
               Country = "country",
               TotalPrice = 99999
            },
            new OrderEntity
            {
               FirstName = "first2",
               LastName = "last2",
               UserName = "user2",
               City = "city",
               Country = "country",
               TotalPrice = 99999
            }
        };
}
