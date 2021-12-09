using Archive.Infrastructure.Persistence;
using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Archive.Infrastructure.Helpers;

public class DbInitializer
{
    private readonly DataContext _context;

    public DbInitializer(DataContext context)
    {
        _context = context;
    }

    public async Task MigrateAndSeed()
    {
        await _context.Database.MigrateAsync();

        if (_context.TodoItems.Any())
            return;

        var priorityData = File.ReadAllText("../Todo.Infrastructure/Helpers/DataForSeed/Priorities.json");
        var priorities = JsonConvert.DeserializeObject<List<Priority>>(priorityData);

        foreach (var priority in priorities)
            _context.Add(priority);

        _context.SaveChanges();
    }   
}
        