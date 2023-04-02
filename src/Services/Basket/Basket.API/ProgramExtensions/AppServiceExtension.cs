using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.DependencyInjection;
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

            var s = config.GetValue<string>("GrpcSettings:DiscountUrl");

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
            {
                o.Address = new Uri(config.GetValue<string>("GrpcSettings:DiscountUrl"));
            });
            services.AddScoped<DiscountGrpcService>();

            return services;
        }
    }
}