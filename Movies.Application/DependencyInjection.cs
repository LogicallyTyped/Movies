using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Mappings;
using System.Reflection;

namespace Movies.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        MappingConfig.Configure();
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(DependencyInjection).Assembly);
        services.AddSingleton(config);

        return services;
    }
}
