using Microsoft.EntityFrameworkCore;
using Todo.Api.Persistence;
using Todo.Api.Persistence.Repositories.Interfaces;
using Todo.Api.Persistence.Repositories;
using Todo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();

builder.Services.AddScoped<ITodosService, TodosService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    .WithOrigins("http://localhost:8080")
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
