namespace Todo.Application.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Contracts;
using Todo.Application.Services;


public static class ServiceCollection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITodosService, TodosService>();
        services.AddScoped<IPriorityService, PriorityService>();

        return services;
    }
}

            