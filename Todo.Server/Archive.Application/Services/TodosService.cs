namespace Archive.Application.Services;

using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Archive.Application.Contracts;
using Archive.Infrastructure.Persistance;
using Microsoft.Extensions.Logging;
using Domain.Common.ResultHandling;
using Domain.Common.ServiceBusDtos;
using MassTransit;
using AutoMapper;

public class TodosService : ITodosService
{
    private readonly ILogger<TodosService> _logger;
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public TodosService(ILogger<TodosService> logger, IMapper mapper, DataContext dataContext, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _dataContext = dataContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Create(TodoItem todoItem)
    {
        _dataContext.Add(todoItem);

        var result = await _dataContext.SaveChangesAsync();

        if (result is 0)
            _logger.LogError($"Creation failed in TodosService for todo with id {todoItem.Id}");
        else
            _logger.LogInformation($"Creation successfull in TodosService for todo with id {todoItem.Id}");
    }

    public async Task Delete(Guid id)
    {
        var todoItem = await _dataContext.FindAsync<TodoItem>(id);

        _dataContext.Remove(todoItem);  
        await _dataContext.SaveChangesAsync();
    }

    public async Task<TodoItem> Get(Guid id)
    {
        return await _dataContext
            .Set<TodoItem>()
            .Include(todo => todo.Priority)
            .FirstOrDefaultAsync(todo => todo.Id == id);
    }

    public async Task<IEnumerable<TodoItem>> GetAll()
    {
        return await _dataContext
            .Set<TodoItem>()
            .Include(todo => todo.Priority)
            .ToListAsync();
    }

    //TODO refactor order
    public async Task<Result> UnarchiveAsync(TodoItem todoItem)
    {
        _logger.LogInformation("Client calling UnarchiveAsync in Service Layer.");

        var fail = Result.Fail("Something went wrong. Could not archive todo.");

        try
        {
            var todoItemForArchiving = _mapper.Map<TodoItemForUnarchiving>(todoItem);

            await _publishEndpoint.Publish<TodoItemForUnarchiving>(todoItem);

            var todoItemFromDb = await _dataContext.FindAsync<TodoItem>(todoItem.Id);

            _dataContext.Remove(todoItem);
            var result = await _dataContext.SaveChangesAsync();

            if (result is 0)
            {
                _logger.LogError($"Deletion failed in TodosService for todo with id {todoItem.Id}");
                return fail;
            }
            else
                _logger.LogInformation($"Deletion successfull in TodosService for todo with id {todoItem.Id}");

        }
        catch (Exception ex)
        {
            _logger.LogError($"ArchiveAsync failed in Service Layer. {ex.Message}");

            return fail;
        }

        _logger.LogInformation("Successfully completed ArchiveAsync in Service Layer.");

        return Result.Ok();
    }
}
