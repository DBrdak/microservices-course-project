using System.Reflection;
using Basket.API.GrpcServices;
using Basket.API.Mappings;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.OpenApi.Models;

namespace Basket.API.ProgramExtensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            });

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration["CacheSettings:ConnectionString"];
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"] ??
                                    throw new InvalidOperationException("Dicounnt URL not found"));
            });
            services.AddScoped<DiscountGrpcService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, configMq) =>
                {
                    configMq.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            return services;
        }
    }
}