using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDataContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>();
        return services;
    }
}