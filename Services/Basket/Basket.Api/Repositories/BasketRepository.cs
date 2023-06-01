using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<Cart> GetUserBasket(string userName)
    {
        string basket = await _cache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket)) return new();

        return JsonConvert.DeserializeObject<Cart>(basket) ?? new();
    }

    public async Task<Cart> UpsertBasket(Cart basket)
    {
        await _cache.SetStringAsync(basket.UserName,
            JsonConvert.SerializeObject(basket));

        return await GetUserBasket(basket.UserName);
    }
    public async Task DeleteBasket(string userName)
    {
       await _cache.RemoveAsync(userName);
    }
}
