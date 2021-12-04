namespace Archive.Api.Services;

using Archive.Api.Entities;
using Archive.Api.Helpers;

public interface ITodosService
{
    Task<TodoItem> Get(Guid id);
    Task<IEnumerable<TodoItem>> GetAll();
    Task Create(TodoItem todoItem);
    Task Delete(Guid id);
}
