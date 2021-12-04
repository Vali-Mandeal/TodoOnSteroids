using Todo.Api.Entities;
using Todo.Api.Persistence;

namespace Todo.Api.Services;

public class PriorityService : IPriorityService
{
    private readonly ILogger<PriorityService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PriorityService(ILogger<PriorityService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Priority> GetAsync(Guid id)
    {
        _logger.LogInformation("Client calling GetAsync in Priority Service Layer.");
        return await _unitOfWork.Priorities.GetAsync(id);
    }
}

