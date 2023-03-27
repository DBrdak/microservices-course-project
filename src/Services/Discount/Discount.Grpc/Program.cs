using Discount.API.ProgramExtensions;
using Discount.Grpc.Data;
using Discount.Grpc.ProgramExtensions;
using Discount.Grpc.Services;

namespace Discount.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.MigrateDatabase<DataContext>();

            app.Run();

            app.MapGrpcService<DiscountService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");




        }
    }
}