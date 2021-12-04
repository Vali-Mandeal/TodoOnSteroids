namespace Todo.Api.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Services;
using Todo.Api.Dtos;
using Todo.Api.Entities;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ITodosService _todosService;
    private readonly IPriorityService _priorityService;
    private readonly IMapper _mapper;

    public TodosController(ILogger<TodosController> logger, IMapper mapper, ITodosService todosService, IPriorityService priorityService)
    {
        _logger = logger;
        _mapper = mapper;
        _todosService = todosService;
        _priorityService = priorityService;
    }


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItemForReadDto>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]   
    public async Task<IActionResult> GetAll()
    {
        var todos = await _todosService.GetAllAsync();

        if (!todos.Any())
            return NoContent();

        var todosForReturn = _mapper.Map<IEnumerable<TodoItemForReadDto>>(todos);

        return Ok(todosForReturn);
    }


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItemForReadDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetTodo")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var todo = await _todosService.GetAsync(id);

        if(todo is null)
            return NotFound();

        var todoForReturn = _mapper.Map<TodoItemForReadDto>(todo);

        return Ok(todoForReturn);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemForReadDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(TodoItemForCreateDto todoItemForCreateDto)
    {
        var priority = await _priorityService.GetAsync(todoItemForCreateDto.PriorityId);
        if (priority is null)
            return BadRequest("Selected priority is invalid.");

        var todoItem = _mapper.Map<TodoItem>(todoItemForCreateDto);
        var result = await _todosService.CreateAsync(todoItem);

        if (result.IsFailure)
            return StatusCode(StatusCodes.Status500InternalServerError, result.Error);

        return CreatedAtRoute("GetTodo", new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(Guid id, TodoItemForUpdateDto todoItemForUpdateDto)
    {
        var priority = await _priorityService.GetAsync(todoItemForUpdateDto.PriorityId);
        if (priority is null)
            return BadRequest("Selected priority is invalid.");

        var todoItem = await _todosService.GetAsync(id);

        if (todoItem is null)
            return NotFound("The todo you're looking for does not exist.");

        var result = await _todosService.UpdateAsync(todoItem, todoItemForUpdateDto);

        if (result.IsFailure)
            return StatusCode(StatusCodes.Status500InternalServerError, result.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var todoItem = await _todosService.GetAsync(id);

        if (todoItem is not null)
            await _todosService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost("{id}/archive")]    
    [ProducesResponseType(StatusCodes.Status204NoContent)]  
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public async Task<IActionResult> Archive(Guid id)
    {
        var todoItem = await _todosService.GetAsync(id);

        if (todoItem is null)
            return NotFound();

        // call service

        throw new NotImplementedException();
    }

    [HttpPost("{id}/archive")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]  
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unarchive(Guid id)
    {

        // call service

        throw new NotImplementedException();
    }
}

    