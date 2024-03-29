﻿namespace Todo.Infrastructure.Persistence;
    
using Domain.Common.Entities;
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

