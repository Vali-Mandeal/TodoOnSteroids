namespace Todo.Infrastructure.Persistence;

using Domain.Common.ResultHandling;
using Todo.Application.Contracts.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public ITodoItemRepository TodoItems { get; set; }      
    public IPriorityRepository Priorities { get; set; }  

    public UnitOfWork(DataContext context, ITodoItemRepository todoItems, IPriorityRepository priorities)
    {
        _context = context;
        TodoItems = todoItems;
        Priorities = priorities;
    }

    public async Task<Result> CompleteAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail($"Error Message: {e.Message}, Inner Exception: {e.InnerException}");
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
