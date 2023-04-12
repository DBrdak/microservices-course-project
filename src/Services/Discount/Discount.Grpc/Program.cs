using Discount.Grpc.ProgramExtensions;
using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace Discount.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);

            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(8001, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
                options.Listen(IPAddress.Any, 80, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
                options.Listen(IPAddress.Any, 5001, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            });

            var app = builder.Build();
            
            app.MigrateDatabase<DataContext>();

            app.MapGrpcService<DiscountService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();


        }
    }
}