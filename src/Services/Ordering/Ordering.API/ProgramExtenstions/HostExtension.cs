using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Peristence;

namespace Ordering.Extenstions;

public static class HostExtenstion
{
    public static async Task<IHost> MigrateDatabase(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<OrderContext>();
            var logger = services.GetService<ILogger<OrderContextSeed>>();
            await context.Database.MigrateAsync();
            await OrderContextSeed.SeedAsync(context, logger);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex.Message, "Error occured during migration");
        }

        return app;
    }
}