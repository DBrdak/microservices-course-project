using Discount.Grpc.Data;
using Discount.Grpc.Mapper;
using Discount.Grpc.Repositories;

namespace Discount.Grpc.ProgramExtensions;

public static class AppServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        //services.AddSingleton<DataContext>();
        services.AddSingleton(new DataContext(config));
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddAutoMapper(typeof(Profiles));

        services.AddGrpc();

        return services;
    }
}