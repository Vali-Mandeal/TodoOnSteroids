namespace Todo.Api.Persistence.Repositories;

using Todo.Api.Persistence.Repositories.Interfaces;
using Todo.Api.Entities;

public class PriorityRepository : Repository<Priority>, IPriorityRepository
{
    private readonly DataContext _context;
    public PriorityRepository(DataContext context)
        : base(context)
    {
        _context = context;
    }
}
        