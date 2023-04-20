using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

namespace OcelotApiGw.ProgramExtensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder ConfigureWebAppBuilder(this WebApplicationBuilder builder, IConfiguration config)
        {
            builder.Logging.AddConfiguration(config.GetSection("Logging"));
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Services.AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle());

            builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
            });

            return builder;
        }
    }
}

