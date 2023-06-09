using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Order.Appliction.Contracts.Persistence;
using Order.Infrastructure.Implementation.Repositories;
using Order.Application.Contracts.Persistence;
using Order.Application.Contracts.Infrastructure;
using Order.Infrastructure.Implementation.Services;

namespace Order.Infrastructure.IoC;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("OrderConnectionString"));
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
