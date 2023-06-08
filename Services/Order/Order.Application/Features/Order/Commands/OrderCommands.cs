using MediatR;

namespace Order.Application.Features.Order.Commands;

public class CheckoutOrderCommand : IRequest<int>
{
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

public class UpdateOrderCommand : IRequest
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

public class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
}
