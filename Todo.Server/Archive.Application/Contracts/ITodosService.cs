namespace Archive.Application.Contracts;

using Domain.Common.Entities;

public interface ITodosService
{
    Task<TodoItem> Get(Guid id);    
    Task<IEnumerable<TodoItem>> GetAll();
    Task Create(TodoItem todoItem);
    Task Delete(Guid id);
}
