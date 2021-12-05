namespace Archive.Application.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Archive.Application.Contracts;
using Archive.Application.Services;
using MassTransit;

public static class ServiceCollection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {

            config.AddConsumer<TodosConsumer>();

            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Url"]);

                cfg.ReceiveEndpoint(configuration["RabbitMq:NotificationServiceQueue"], c =>
                {
                    c.ConfigureConsumer<TodosConsumer>(ctx);
                });
            });
        });

        services.AddMassTransitHostedService();

        services.AddScoped<ITodosService, TodosService>();

        return services;
    }
}
