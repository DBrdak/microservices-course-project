﻿using Discount.API.Data;
using Npgsql;

namespace Discount.API.ProgramExtensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        var retryForAvailability = retry.Value;

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataContext>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migrating postegresql database");

            using var connection = context.CreateConnection();
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
            command.ExecuteNonQuery();

            command.CommandText =
                "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
            command.ExecuteNonQuery();

            command.CommandText =
                "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();

            logger.LogInformation("Migrated postresql database.");
        }
        catch (Exception e)
        {
            logger.LogError(e.Message, "DiscountDb migration failed");

            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<DataContext>(host, retryForAvailability);
            }
        }

        return host;
    }
}