using Order.Application.Contracts.Infrastructure;
using Order.Application.Models;

namespace Order.Infrastructure.Implementation.Services;

public class EmailService : IEmailService
{
    public Task<bool> SendEmail(Email email)
    {
        throw new NotImplementedException();
    }
}
