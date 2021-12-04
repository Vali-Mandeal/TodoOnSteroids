namespace Todo.Api.Services;

using Todo.Api.Entities;
using Todo.Api.Dtos;
using Todo.Api.Helpers;
using Todo.Api.Persistence;
using AutoMapper;

public class TodosService : ITodosService
{
    ILogger<TodosService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TodosService(ILogger<TodosService> logger, IMapper mapper,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public Task<TodoItem> GetAsync(Guid id)
    {
        _logger.LogInformation("Client calling GetAsync in Service Layer.");
        return _unitOfWork.TodoItems.GetAsync(id);
    }

    public Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        _logger.LogInformation("Client calling GetAllAsync in Service Layer.");
        return _unitOfWork.TodoItems.GetAllAsync();
    }

    public Task<Result> ArchiveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TodoItem>> CreateAsync(TodoItem todoItem)
    {
        _logger.LogInformation("Client calling CreateAsync in Service Layer.");

        _unitOfWork.TodoItems.Add(todoItem);
        var result = await _unitOfWork.CompleteAsync();

        if (result.IsFailure)
        {
            _logger.LogError($"CreateAsync failed in Service Layer.{result.Error}");
            return Result<TodoItem>.Fail("Could not create Todo.");
        }

        var createdTodo = await _unitOfWork.TodoItems.GetAsync(todoItem.Id);

        _logger.LogInformation($"CreateAsync succesful in Service Layer for TodoId: {createdTodo.Id}");

        return Result<TodoItem>.Ok(createdTodo);
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Client calling DeleteAsync in Service Layer.");

        var todoItem = await _unitOfWork.TodoItems.GetAsync(id);

        _unitOfWork.TodoItems.Remove(todoItem);
        await _unitOfWork.CompleteAsync();
    }

    public Task<Result> UnarchiveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateAsync(TodoItem todoItem, TodoItemForUpdateDto todoItemForUpdateDto)
    {
        _logger.LogInformation("Client calling UpdateAsync in Service Layer.");

        _mapper.Map(todoItemForUpdateDto, todoItem);

        var result = await _unitOfWork.CompleteAsync();

        if (result.IsFailure)
        {
           _logger.LogError($"UpdateAsync failed in Service Layer.{result.Error}");
            return Result.Fail("Could not update todo.");
        }

        return Result.Ok();
    }
}
