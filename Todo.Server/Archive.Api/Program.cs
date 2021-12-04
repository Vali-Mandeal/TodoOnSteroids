using Microsoft.EntityFrameworkCore;
using Archive.Api.Persistance;
using Archive.Api.Services;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
    .WithOrigins("http://localhost:5125")
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
