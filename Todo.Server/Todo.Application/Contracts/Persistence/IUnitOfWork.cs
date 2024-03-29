﻿namespace Todo.Application.Contracts.Persistence;

using Domain.Common.ResultHandling;

public interface IUnitOfWork : IDisposable
{
    ITodoItemRepository TodoItems { get; }  
    IPriorityRepository Priorities { get; }
 
    Task<Result> CompleteAsync();
}   