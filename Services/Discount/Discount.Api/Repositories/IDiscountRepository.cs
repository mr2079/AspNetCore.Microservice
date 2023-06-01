using Discount.Api.Entities;

namespace Discount.Api.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(int id);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(int id);
}
