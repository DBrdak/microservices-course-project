using Discount.API.Data;
using Discount.API.Repositories;
using Microsoft.OpenApi.Models;

namespace Discount.API.ProgramExtensions;

public static class AppServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });

        services.AddSingleton<DataContext>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();


        return services;
    }
}