namespace Todo.Api.Persistence;

using Todo.Api.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options)
           : base(options)
    {
    }
        
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Priority> Priorities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
