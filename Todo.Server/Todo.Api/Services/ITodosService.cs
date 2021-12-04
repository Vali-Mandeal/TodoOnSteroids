namespace Todo.Api.Services;

using Todo.Api.Entities;
using Todo.Api.Dtos;
using Todo.Api.Helpers;

public interface ITodosService
{
    Task<TodoItem> GetAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<Result<TodoItem>> CreateAsync(TodoItem todoItem);
    Task<Result> UpdateAsync(TodoItem todoItem, TodoItemForUpdateDto todoItemForUpdateDto); 
    Task DeleteAsync(Guid id);
    Task<Result> ArchiveAsync(Guid id); 
    Task<Result> UnarchiveAsync(Guid id);
}
            