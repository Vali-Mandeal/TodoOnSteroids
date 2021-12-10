namespace Todo.Application.Services;

using Domain.Common.Entities;
using Domain.Common.ServiceBusDtos;
using Domain.Common.ResultHandling;
using Todo.Application.Contracts;
using Todo.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MassTransit;
using Todo.Application.Contracts.RedisCache;
using System.Text.Json;

public class TodosService : ITodosService
{
    private readonly ILogger<TodosService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRedisRepository _redisRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private const string RedisCacheKeyForList = "all";

    public TodosService(
            ILogger<TodosService> logger, 
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IRedisRepository redisRepository, 
            IPublishEndpoint publishEndpoint
        )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _redisRepository = redisRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<TodoItem> GetAsync(Guid id, bool disableCache = false)
    {
        _logger.LogInformation("Client calling GetAsync in Service Layer...");
        TodoItem todo;

        var cacheValue = await _redisRepository.GetStringValue(id.ToString());

        if (cacheValue is not null)
        {
            todo = JsonSerializer.Deserialize<TodoItem>(cacheValue);
            _logger.LogInformation("GetAsync, value found in cache.");
            return todo;
        }

        _logger.LogInformation("GetAsync, value not found in cache. Calling SQL...");
        todo = await _unitOfWork.TodoItems.GetAsync(id);

        if (!disableCache)
            await _redisRepository.SetStringValue(id.ToString(), JsonSerializer.Serialize(todo));

        return todo;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        _logger.LogInformation("Client calling GetAllAsync in Service Layer...");
        IEnumerable<TodoItem> todoItems;

        var cacheValue = await _redisRepository.GetStringValue(RedisCacheKeyForList);

        if (cacheValue is not null)
        {
            todoItems = JsonSerializer.Deserialize<IEnumerable<TodoItem>>(cacheValue);
            _logger.LogInformation("GetAllAsync, value found in cache.");
            return todoItems;
        }

        _logger.LogInformation("GetAsync, value not found in cache. Calling SQL...");
        todoItems =  await _unitOfWork.TodoItems.GetAllAsync();

        await _redisRepository.SetStringValue(RedisCacheKeyForList, JsonSerializer.Serialize(todoItems));

        return todoItems;
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

        await _redisRepository.DeleteStringValue(RedisCacheKeyForList);

        _logger.LogInformation($"CreateAsync succesful in Service Layer for TodoId: {createdTodo.Id}");

        return Result<TodoItem>.Ok(createdTodo);
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

        await _redisRepository.DeleteStringValue(oldTodoItem.Id.ToString());
        await _redisRepository.DeleteStringValue(RedisCacheKeyForList);

        return Result.Ok();
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Client calling DeleteAsync in Service Layer.");

        var todoItem = await _unitOfWork.TodoItems.GetAsync(id);

        if (todoItem is null)
            return;

        _unitOfWork.TodoItems.Remove(todoItem);
        await _unitOfWork.CompleteAsync();

        await _redisRepository.DeleteStringValue(id.ToString());
        await _redisRepository.DeleteStringValue(RedisCacheKeyForList);
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

        await _redisRepository.DeleteStringValue(todoItem.Id.ToString());
        await _redisRepository.DeleteStringValue(RedisCacheKeyForList);

        _logger.LogInformation($"Successfully completed ArchiveAsync  for todo with id {todoItem.Id} in Service Layer.");

        return Result.Ok();
    }
}
