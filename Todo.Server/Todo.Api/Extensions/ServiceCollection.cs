namespace Todo.Api.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Persistence;
using Todo.Application.Contracts.Persistence;
using Todo.Infrastructure.Persistence.Repositories;

public static class ServiceCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<IPriorityRepository, PriorityRepository>();

        return services;
    }
}
        