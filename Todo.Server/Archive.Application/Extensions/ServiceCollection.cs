namespace Archive.Application.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Archive.Application.Contracts;
using Archive.Application.Services;

public static class ServiceCollection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITodosService, TodosService>();

        return services;
    }
}
