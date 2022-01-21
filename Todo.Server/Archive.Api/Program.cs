using Archive.Api.Hubs;
using Archive.Application.Extensions;
using Archive.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLogging();

builder.Services.AddSignalR();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);

builder.Services.AddMvc();
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
    //corsBuilder
    //.WithOrigins(builder.Configuration["Cors:TodoUrl"], builder.Configuration["Cors:ClientUrl"])
    //.AllowAnyMethod()
    //.AllowAnyHeader();
    corsBuilder
      .SetIsOriginAllowed(origin => true)
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials();
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ArchiveHub>("/archiveHub");
});

app.MapControllers();

app.Run();