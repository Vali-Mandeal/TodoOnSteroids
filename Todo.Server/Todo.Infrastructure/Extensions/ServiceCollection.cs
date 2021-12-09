namespace Todo.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Helpers;
using StackExchange.Redis;
using Todo.Application.Contracts.RedisCache;
using Todo.Infrastructure.Persistence.RedisCache;
using Todo.Application.Contracts.Persistence;
using Todo.Infrastructure.Persistence.Repositories;

public static class ServiceCollection   
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("10.0.75.1");

        IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        services.AddScoped(s => redis.GetDatabase());

        services.AddScoped<DbInitializer>();

        services.AddDbContextPool<DataContext>(options =>
            options.UseSqlServer
            (
                configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
            ));

        var serviceProvider = services.BuildServiceProvider();
        var dataContext = serviceProvider.GetService<DataContext>();

        dataContext.Database.Migrate();

        services.AddScoped<IRedisRepository, RedisRepository>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<IPriorityRepository, PriorityRepository>();

        return services;
    }
}
