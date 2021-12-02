namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger _logger;
    public TodosController(ILogger<TodosController> logger)
    {
        _logger = logger;
    }


    [HttpGet]   
    public async Task<IActionResult> GetTest()
    {
        Random random = new Random();
        int value = random.Next(1, 10);

        _logger.Log(LogLevel.Information, $"Returning random value: {value}");

        return Ok(value);
    }
}

    