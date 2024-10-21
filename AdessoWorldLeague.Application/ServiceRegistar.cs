using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AdessoWorldLeague.Application;

public static class ServiceRegistar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(),includeInternalTypes:true);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistar).Assembly);
        });
        return services;
    }
}