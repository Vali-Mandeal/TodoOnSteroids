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
        var todoItems = await _todosService.GetAll();

        if (!todoItems.Any())
            return NoContent();

        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var todoItem = await _todosService.Get(id);

        if (todoItem is null)
            return NotFound();

        return Ok(todoItem);
    }


}
