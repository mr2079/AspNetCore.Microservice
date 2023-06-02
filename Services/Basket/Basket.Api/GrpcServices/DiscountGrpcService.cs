using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProto.DiscountProtoClient _discountProtoService;

    public DiscountGrpcService(DiscountProto.DiscountProtoClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        
        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
}
