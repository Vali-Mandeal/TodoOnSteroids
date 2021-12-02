namespace Todo.Api.Persistence.Repositories;

using Todo.Api.Persistence.Repositories.Interfaces;
using Todo.Api.Entities;

public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
{
    private readonly DataContext _context;
    public TodoItemRepository(DataContext context)
        : base(context)
    {
        _context = context;
    }
}
    