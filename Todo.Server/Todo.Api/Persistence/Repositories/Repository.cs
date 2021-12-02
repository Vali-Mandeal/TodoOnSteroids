namespace Todo.Api.Persistence.Repositories;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Persistence.Repositories.Interfaces;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DataContext Context;

    public Repository(DataContext context)
    {
        Context = context;
    }
    public virtual async Task<TEntity> GetAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>()
            .Where(predicate)
            .ToListAsync();
    }

    public virtual void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}
