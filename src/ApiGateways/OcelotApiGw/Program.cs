using Microsoft.Extensions.Logging.Configuration;
using Ocelot.Middleware;
using OcelotApiGw.ProgramExtensions;

namespace OcelotApiGw
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureWebAppBuilder(builder.Configuration);

            var app = builder.Build();

            app.UseRouting();

            app.MapGet("/", () => "Hello World!");
            
            await app.UseOcelot();
            await app.RunAsync();
        }
    }
}