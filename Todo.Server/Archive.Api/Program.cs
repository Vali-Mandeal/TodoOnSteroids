using Archive.Application.Extensions;
using Archive.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLogging();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);

builder.Services.AddCors();

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
