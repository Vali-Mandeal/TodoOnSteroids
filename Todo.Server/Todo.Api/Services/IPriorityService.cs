namespace Todo.Api.Services;

using Todo.Api.Entities;

public interface IPriorityService
{
    Task<Priority> GetAsync(Guid id);
}

