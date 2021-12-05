namespace Todo.Application.Contracts.Persistence;

//using Todo.Api.Persistence.Repositories.Interfaces;
using Domain.Common.ResultHandling;

public interface IUnitOfWork : IDisposable
{
    ITodoItemRepository TodoItems { get; }  
    IPriorityRepository Priorities { get; }
 
    Task<Result> CompleteAsync();
}   