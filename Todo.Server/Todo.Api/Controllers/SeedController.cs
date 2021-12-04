namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Todo.Api.Persistence; 
using Microsoft.EntityFrameworkCore;
using Todo.Api.Helpers;

[Route("api/[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly DataContext _context;

    public SeedController(ILogger<SeedController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

        
    [HttpPost]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            _logger.LogInformation("Seeding database...");

            _context.Database.Migrate();
            DbInitializer.SeedData(_context);
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

