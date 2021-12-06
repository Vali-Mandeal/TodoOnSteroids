namespace Todo.Application.Services;

using Domain.Common.Entities;
using Domain.Common.ServiceBusDtos;
using Domain.Common.ResultHandling;
using Todo.Application.Contracts;
using Todo.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MassTransit;

public class TodosService : ITodosService
{
    private readonly ILogger<TodosService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;

    public TodosService(ILogger<TodosService> logger, IMapper mapper, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<TodoItem> GetAsync(Guid id)
    {
        _logger.LogInformation("Client calling GetAsync in Service Layer.");
        return await _unitOfWork.TodoItems.GetAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        _logger.LogInformation("Client calling GetAllAsync in Service Layer.");
        return await _unitOfWork.TodoItems.GetAllAsync();
    }

    public async Task<Result> ArchiveAsync(TodoItem todoItem)
    {
        _logger.LogInformation("Client calling ArchiveAsync in Service Layer.");

        _unitOfWork.TodoItems.Remove(todoItem);
        var result = await _unitOfWork.CompleteAsync();

        if (result.Success)
        {
            var todoItemForArchiving = _mapper.Map<TodoItemForArchiving>(todoItem);
            await _publishEndpoint.Publish<TodoItemForArchiving>(todoItemForArchiving);
        }
        else
        {
            _logger.LogError($"ArchiveAsync failed in Service Layer. {result.Error}");
            return Result.Fail("Something went wrong. Could not archive todo.");
        }
   
        _logger.LogInformation($"Successfully completed ArchiveAsync  for todo with id {todoItem.Id} in Service Layer.");

        return Result.Ok();
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

    public async Task<Result> UpdateAsync(TodoItem oldTodoItem, TodoItem newTodoItem)
    {
        _logger.LogInformation("Client calling UpdateAsync in Service Layer.");

        _mapper.Map(newTodoItem, oldTodoItem);

        var result = await _unitOfWork.CompleteAsync();

        if (result.IsFailure)   
        {
            _logger.LogError($"UpdateAsync failed in Service Layer.{result.Error}");
            return Result.Fail("Could not update todo.");
        }

        return Result.Ok();
    }
}
