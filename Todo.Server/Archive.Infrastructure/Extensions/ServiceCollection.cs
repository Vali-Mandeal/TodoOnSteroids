namespace Archive.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Archive.Infrastructure.Persistance;
using MassTransit;
using Archive.Infrastructure.ServiceBus;

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

        return services;
    }
}
