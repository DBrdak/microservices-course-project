using Basket.API.Repositories;
using Microsoft.OpenApi.Models;

namespace Basket.API.ProgramExtensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            });

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = config.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}