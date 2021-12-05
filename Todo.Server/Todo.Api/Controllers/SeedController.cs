namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Helpers;
using Domain.Common.CustomFilters;

[Route("api/[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly DbInitializer _dbInitializer;

    public SeedController(ILogger<SeedController> logger, DbInitializer dbInitializer)
    {
        _logger = logger;
        _dbInitializer = dbInitializer;
    }

    [TypeFilter(typeof(DevOnlyActionFilter))]
    [HttpPost]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            _logger.LogInformation("Seeding database...");

            await _dbInitializer.MigrateAndSeed();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred during migration. Exception: {ex.Message}{Environment.NewLine}" +
               $"Inner exception: {ex.InnerException}{Environment.NewLine}" +
               $"Source: {ex.Source}{Environment.NewLine}" +
               $"Stacktrace: {ex.StackTrace}{Environment.NewLine}");

            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }

        return Ok("Database migrated successfully!");
    }
}
    
