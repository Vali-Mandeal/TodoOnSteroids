namespace Archive.Infrastructure.Persistence.Repositories;

using Archive.Application.Contracts.Persistence;
using Domain.Common.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
{
    private readonly DataContext _context;
    public TodoItemRepository(DataContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems
            .Include(todo => todo.Priority)
            .ToListAsync();
    }

    public override async Task<TodoItem> GetAsync(Guid id)
    {
        return await _context.TodoItems
            .Include(_todo => _todo.Priority)
            .FirstOrDefaultAsync(todo => todo.Id == id);
    }
}
