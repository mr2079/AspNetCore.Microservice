namespace Basket.Api.Entities;

public class Cart
{
    public Cart() {}
    public Cart(string userName) { UserName = userName; }

    public string UserName { get; set; } = string.Empty;
    public List<CartItem>? Items { get; set; }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            if (Items != null && Items.Any())
                foreach (CartItem item in Items)
                    totalPrice += item.Price * item.Quantity;
            return totalPrice;
        }
    }
}
 