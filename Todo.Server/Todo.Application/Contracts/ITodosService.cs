namespace Todo.Application.Contracts;

using Domain.Common.Entities;
//using Todo.Api.Dtos;
using Domain.Common.ResultHandling;

public interface ITodosService
{
    Task<TodoItem> GetAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<Result<TodoItem>> CreateAsync(TodoItem todoItem);
    //Task<Result> UpdateAsync(TodoItem todoItem, TodoItemForUpdateDto todoItemForUpdateDto); 
    Task DeleteAsync(Guid id);
    Task<Result> ArchiveAsync(Guid id); 
    Task<Result> UnarchiveAsync(Guid id);
}
            