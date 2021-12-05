namespace Todo.Application.Contracts;

using Domain.Common.Entities;
using Domain.Common.ResultHandling;

public interface ITodosService
{
    Task<TodoItem> GetAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<Result<TodoItem>> CreateAsync(TodoItem todoItem);
    Task<Result> UpdateAsync(TodoItem oldTodoItem, TodoItem newTodoItem);
    Task DeleteAsync(Guid id);
    Task<Result> ArchiveAsync(TodoItem todoItem); 
}
            