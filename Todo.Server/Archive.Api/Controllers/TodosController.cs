using Microsoft.AspNetCore.Mvc;
using Archive.Application.Contracts;

namespace Archive.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly ITodosService _todosService;

    public TodosController(ILogger<TodosController> logger, ITodosService todosService)
    {
        _logger = logger;
        _todosService = todosService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todoItems = await _todosService.GetAllAsync();

        if (!todoItems.Any())
            return NoContent();

        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var todoItem = await _todosService.GetAsync(id);

        if (todoItem is null)
            return NotFound();

        return Ok(todoItem);
    }

    [HttpPost("{id}/unarchive")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unarchive(Guid id)
    {
        var todoItem = await _todosService.GetAsync(id);

        if (todoItem is null)
            return NotFound();

        var result = await _todosService.UnarchiveAsync(todoItem);

        if (result.IsFailure)
            return StatusCode(StatusCodes.Status500InternalServerError, result.Error);

        return NoContent();
    }
}
