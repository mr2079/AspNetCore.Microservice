namespace Order.Application.DTOs;

public class OrdersDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    public string BankName { get; set; } = string.Empty;
    public string RefCode { get; set; } = string.Empty;
    public int PaymentMethod { get; set; }
}
