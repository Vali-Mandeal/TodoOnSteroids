namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Helpers;
using Domain.Common.CustomFilters;
using Microsoft.AspNetCore.SignalR;
using Todo.Api.Hubs;
using Todo.Application.Contracts;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ITodosService _todosService;
    private readonly IHubContext<TodoHub> _hubContext;

    public TestController(ILogger<TestController> logger, ITodosService todosService, IHubContext<TodoHub> hubContext)
    {
        _logger = logger;   
        _todosService = todosService;
        _hubContext = hubContext;
    }

    [TypeFilter(typeof(DevOnlyActionFilter))]
    [HttpPost]
    public async Task<IActionResult> DoSomething()
    {
        try
        {
            var todoItems = await _todosService.GetAllAsync();
            var todoItem = todoItems.FirstOrDefault();


            _logger.LogInformation($"Started Publishing SignalREvent for {todoItem?.Description}");


            await _hubContext.Clients.All.SendAsync("ArchivedTodo", todoItem);

            _logger.LogInformation("Finished Publishing SignalREvent");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

        return Ok("Published SignalR Event");
    }
}
    
