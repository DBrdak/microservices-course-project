using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.ProgramExtensions
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(config["ApiSettings:CatalogUrl"]));


            services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(config["ApiSettings:OrderingUrl"]));


            services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(config["ApiSettings:BasketUrl"]));

            services.AddScoped<IAggregatorService, AggregatorService>();

            //services.AddHealthChecks()
            //    .AddUrlGroup(new Uri($"{Configuration["ApiSettings:CatalogUrl"]}/swagger/index.html"), "Catalog.API", HealthStatus.Degraded)
            //    .AddUrlGroup(new Uri($"{Configuration["ApiSettings:BasketUrl"]}/swagger/index.html"), "Basket.API", HealthStatus.Degraded)
            //    .AddUrlGroup(new Uri($"{Configuration["ApiSettings:OrderingUrl"]}/swagger/index.html"), "Ordering.API", HealthStatus.Degraded);

            return services;
        }
    }
}
