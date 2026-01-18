using Microsoft.Extensions.DependencyInjection;

namespace Delta.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeltaShared(this IServiceCollection services)
    {
        return services;
    }
}
