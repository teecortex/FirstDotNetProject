using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, string conn_string)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(conn_string));
        return services;
    }
}