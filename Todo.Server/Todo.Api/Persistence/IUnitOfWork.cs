namespace Todo.Api.Persistence;

using Todo.Api.Persistence.Repositories.Interfaces;
using Todo.Api.Helpers;

public interface IUnitOfWork : IDisposable
{
    ITodoItemRepository TodoItems { get; }  
    IPriorityRepository Priorities { get; }
 
    Task<Result> CompleteAsync();
}   