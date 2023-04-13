using System.Net;
using Discount.Grpc.Data;
using Discount.Grpc.ProgramExtensions;
using Discount.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Discount.Grpc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddServices(builder.Configuration);

        var app = builder.Build();

        app.MigrateDatabase<DataContext>();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<DiscountService>();

            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync(
                    "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            });
        });

        await app.RunAsync();
    }
}