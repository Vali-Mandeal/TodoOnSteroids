using Microsoft.EntityFrameworkCore;
using Archive.Api.Persistance;
using Archive.Api.Services;
using MassTransit;
using GreenPipes;
using Archive.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<ITodosService, TodosService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<TodosConsumer>(); 
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
//    {
//        config.Host(new Uri("rabbitmq://localhost"), h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//        config.ReceiveEndpoint("todoQueue", ep =>
//        {
//            ep.PrefetchCount = 16;
//            ep.UseMessageRetry(r => r.Interval(2, 100));
//            ep.ConfigureConsumer<TodosConsumer>(provider);
//        });
//    }));
//});

builder.Services.AddMassTransit(config => {

    config.AddConsumer<TodosConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host("amqp://guest:guest@localhost:5672");

        cfg.ReceiveEndpoint("todoQueue", c => {
            c.ConfigureConsumer<TodosConsumer>(ctx);
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .WithOrigins("http://localhost:5125")
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
