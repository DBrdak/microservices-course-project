using Microsoft.Extensions.Logging;
using Ordering.Domain.Entity;

namespace Ordering.Infrastructure.Peristence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation($"Seed database associated with context {typeof(OrderContext).Name}");
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "tontav8@gmail.com",
                AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350
            }
        };
    }
}