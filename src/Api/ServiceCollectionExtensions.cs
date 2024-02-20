namespace Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });

        services.AddEndpointsApiExplorer();

        services.AddMemoryCache();

        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "PracticalCleanArchitecture API";
        });

        return services;
    }
}
