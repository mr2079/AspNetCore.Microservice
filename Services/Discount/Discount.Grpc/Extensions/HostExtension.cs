using Npgsql;

namespace Discount.Grpc.Extensions;

public static class HostExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        int retryForAvailability = retry.Value;

        using (var scope = host.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            IConfiguration configuration = services.GetRequiredService<IConfiguration>();
            ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migration postgresql database");

                using var connection = new NpgsqlConnection(
                    configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

                connection.Open();

                using var command = new NpgsqlCommand() { Connection = connection };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(
                        Id SERIAL PROMARY,
                        ProductName VARCHAR(200) NOT NULL,
                        Description TEXT,
                        Amount INT
                    )";
                command.ExecuteNonQuery();

                // Seed data
                command.CommandText = @"INSERT INTO Coupon (ProductName, Description, Amount)
                    VALUES ('Iphone 14', 'Iphone 14 discount desc', 120)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO Coupon (ProductName, Description, Amount)
                    VALUES ('Samsung S23 Ultra', 'Samsung S23 Ultra discount desc', 120)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO Coupon (ProductName, Description, Amount)
                    VALUES ('Redmi Note 11 Pro+', 'N11P+ discount desc', 220)";
                command.ExecuteNonQuery();

                logger.LogInformation("Migration has been completed");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(@"An error has been occured.
                    Error detail: " + ex.Message);

                if (retryForAvailability < 50) 
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }
        }

        return host;
    }
}
