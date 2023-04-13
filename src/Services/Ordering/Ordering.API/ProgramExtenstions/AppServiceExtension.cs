using System.Reflection;
using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.EventBusConsumer;

namespace Ordering.API.ProgramExtenstions;

public static class AppServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<BasketCheckoutConsumer>();

        services.AddMassTransit(config =>
        {
            config.AddConsumer<BasketCheckoutConsumer>();
            config.UsingRabbitMq((context, configMq) =>
            {
                configMq.Host(configuration["EventBusSettings:HostAddress"]);
                configMq.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue,
                    configEndpoint => { configEndpoint.ConfigureConsumer<BasketCheckoutConsumer>(context); });
            });
        });

        return services;
    }
}