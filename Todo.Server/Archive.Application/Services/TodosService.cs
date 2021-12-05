namespace Archive.Application.Services;

using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Archive.Application.Contracts;
using Archive.Infrastructure.Persistance;

public class TodosService : ITodosService
{
    private readonly DataContext _dataContext;

    public TodosService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Create(TodoItem todoItem)
    {
        _dataContext.Add(todoItem);
        await _dataContext.SaveChangesAsync();
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
}
