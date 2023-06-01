using Basket.Api.Entities;

namespace Basket.Api.Repositories;

public interface IBasketRepository
{
    Task<Cart> GetUserBasket(string userName);
    Task<Cart> UpsertBasket(Cart basket);
    Task DeleteBasket(string userName);
}
