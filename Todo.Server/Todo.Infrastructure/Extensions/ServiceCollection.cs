namespace Todo.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Helpers;

public static class ServiceCollection   
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbInitializer>();

        services.AddDbContextPool<DataContext>(options =>
            options.UseSqlServer
            (
                configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
            ));

        return services;
    }
}
