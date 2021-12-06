namespace Archive.Api.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Archive.Application.Contracts.Persistence;
using Archive.Infrastructure.Persistence.Repositories;

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
