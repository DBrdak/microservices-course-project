
using Basket.API.ProgramExtensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace Basket.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);

            //builder.WebHost.UseKestrel(options =>
            //{
            //    options.ListenAnyIP(8003, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            //    options.Listen(IPAddress.Any, 80, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            //    options.Listen(IPAddress.Any, 5003, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            //});

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}