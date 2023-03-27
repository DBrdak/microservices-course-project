using Discount.Grpc.Data;
using Discount.Grpc.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Discount.Grpc.Mapper;

namespace Discount.Grpc.ProgramExtensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddGrpc();
            
            services.AddSingleton<DataContext>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddAutoMapper(typeof(Profiles));

            return services;
        }
    }
}
