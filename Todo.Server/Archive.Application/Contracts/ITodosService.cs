namespace Archive.Application.Contracts;

using Domain.Common.Entities;
using Domain.Common.ResultHandling;

public interface ITodosService
{
    Task<TodoItem> GetAsync(Guid id);    
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task CreateAsync(TodoItem todoItem);
    Task<Result> UnarchiveAsync(TodoItem todoItem);
}
