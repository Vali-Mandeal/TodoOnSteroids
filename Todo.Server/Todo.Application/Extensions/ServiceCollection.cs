namespace Todo.Application.Extensions;

using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Contracts;
using Todo.Application.Services;

public static class ServiceCollection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddMassTransit(config => {
        //    config.UsingRabbitMq((ctx, cfg) => {
        //        cfg.Host(configuration["RabbitMq:Url"]);
        //    });
        //});

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
        services.AddScoped<IPriorityService, PriorityService>();

        return services;
    }
}

            