namespace Archive.Application.Contracts;

using Domain.Common.Entities;
using Domain.Common.ResultHandling;

public interface ITodosService
{
    Task<TodoItem> Get(Guid id);    
    Task<IEnumerable<TodoItem>> GetAll();
    Task Create(TodoItem todoItem);
    Task Delete(Guid id);
    Task<Result> UnarchiveAsync(TodoItem todoItem);
}
