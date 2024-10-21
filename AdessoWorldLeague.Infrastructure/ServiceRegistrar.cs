using AdessoWorldLeague.Domain.Common.Abstractions;
using AdessoWorldLeague.Infrastructure.Persistence;
using AdessoWorldLeague.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdessoWorldLeague.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration) {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddDbContext<AdessoWorldLeagueDbContext>(cfg =>
        {
            cfg.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
}