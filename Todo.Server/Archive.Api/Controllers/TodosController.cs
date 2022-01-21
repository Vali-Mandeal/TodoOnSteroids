using Microsoft.AspNetCore.Mvc;
using Archive.Application.Contracts;
using Microsoft.AspNetCore.SignalR;
using Archive.Api.Hubs;

namespace Archive.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly ITodosService _todosService;
    private readonly IHubContext<ArchiveHub> _hubContext;

    public TodosController(ILogger<TodosController> logger, ITodosService todosService, IHubContext<ArchiveHub> hubContext)
    {
        _logger = logger;
        _todosService = todosService;
        _hubContext = hubContext;
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

        await _hubContext.Clients.All.SendAsync("UnarchivedTodo", id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _todosService.DeleteAsync(id);

        await _hubContext.Clients.All.SendAsync("DeletedTodo", id);

        return NoContent();
    }
}
