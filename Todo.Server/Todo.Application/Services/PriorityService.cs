namespace Todo.Application.Services;

using Domain.Common.Entities;
using Todo.Application.Contracts;
using Todo.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

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

