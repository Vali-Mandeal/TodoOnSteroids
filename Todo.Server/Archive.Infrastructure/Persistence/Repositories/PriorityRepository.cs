﻿namespace Archive.Infrastructure.Persistence.Repositories;

using Archive.Application.Contracts.Persistence;
using Domain.Common.Entities;

public class PriorityRepository : Repository<Priority>, IPriorityRepository
{
    private readonly DataContext _context;
    public PriorityRepository(DataContext context)
        : base(context)
    {
        _context = context;
    }
}
