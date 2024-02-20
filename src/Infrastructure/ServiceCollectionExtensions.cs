using Application.Common.Abstractions.Caching;
using Infrastructure.Caching;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, PublishDomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, SoftDeletableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();

            options.AddInterceptors(interceptors);

            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddSingleton(TimeProvider.System);

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
