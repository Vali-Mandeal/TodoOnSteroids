using Archive.Application.Extensions;
using Archive.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();

//builder.Services.AddDbContextPool<DataContext>(options =>
//    options.UseSqlServer
//    (
//        builder.Configuration.GetConnectionString("DefaultConnection")
//    ));

//builder.Services.AddScoped<ITodosService, TodosService>();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMassTransit(config => {

//    config.AddConsumer<TodosConsumer>();

//    config.UsingRabbitMq((ctx, cfg) => {
//        cfg.Host("amqp://guest:guest@localhost:5672");

//        cfg.ReceiveEndpoint("todoQueue", c => {
//            c.ConfigureConsumer<TodosConsumer>(ctx);
//        });
//    });
//});

//builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsBuilder =>
{
    corsBuilder
    .WithOrigins(builder.Configuration["Cors:OriginUrl"])
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
