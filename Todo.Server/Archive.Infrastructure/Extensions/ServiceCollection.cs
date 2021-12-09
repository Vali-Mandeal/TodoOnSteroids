namespace Archive.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Archive.Infrastructure.Persistence;
using Archive.Application.Contracts.Persistence;
using Archive.Infrastructure.Persistence.Repositories;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<DataContext>(options =>
            options.UseSqlServer
            (
                configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
            ));

        var serviceProvider = services.BuildServiceProvider();
        var dataContext = serviceProvider.GetService<DataContext>();

        dataContext.Database.Migrate();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<IPriorityRepository, PriorityRepository>();

        return services;
    }
}
