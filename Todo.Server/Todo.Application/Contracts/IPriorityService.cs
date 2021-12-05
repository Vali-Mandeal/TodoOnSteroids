namespace Todo.Application.Contracts;

using Domain.Common.Entities;

public interface IPriorityService
{
    Task<Priority> GetAsync(Guid id);
}

