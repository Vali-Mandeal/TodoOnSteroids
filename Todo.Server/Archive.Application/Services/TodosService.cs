namespace Archive.Application.Services;

using Domain.Common.Entities;
using Archive.Application.Contracts;
using Microsoft.Extensions.Logging;
using Domain.Common.ResultHandling;
using Domain.Common.ServiceBusDtos;
using MassTransit;
using AutoMapper;
using Archive.Application.Contracts.Persistence;

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

    public async Task CreateAsync(TodoItem todoItem)
    {
        _unitOfWork.TodoItems.Add(todoItem);

        var result = await _unitOfWork.CompleteAsync();

        if (result.IsFailure)
            _logger.LogError($"Creation failed in TodosService for todo with id {todoItem.Id}: {result.Error}");
        else
            _logger.LogInformation($"Creation successfull in TodosService for todo with id {todoItem.Id}");
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Client calling DeleteAsync in Service Layer.");

        var todoItem = await _unitOfWork.TodoItems.GetAsync(id);

        _unitOfWork.TodoItems.Remove(todoItem);
        await _unitOfWork.CompleteAsync();
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

    //TODO refactor order
    public async Task<Result> UnarchiveAsync(TodoItem todoItem)
    {
        _logger.LogInformation("Client calling UnarchiveAsync in Service Layer.");

        _unitOfWork.TodoItems.Remove(todoItem);
        var result = await _unitOfWork.CompleteAsync();

        if (result.Success)
        {
            var todoItemForArchiving = _mapper.Map<TodoItemForUnarchiving>(todoItem);
            await _publishEndpoint.Publish<TodoItemForUnarchiving>(todoItemForArchiving);
        }

        else
        {
            _logger.LogError($"UnarchiveAsync failed in Service Layer for todo with id {todoItem.Id}: {result.Error}");
            return Result.Fail("Something went wrong. Could not unarchive todo.");
        }
    
        _logger.LogInformation($"Successfully completed Unarchiveasync for todo with id {todoItem.Id} in Service Layer.");

        return Result.Ok();
    }
}