using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Module.Shared.Domain;

namespace Module.Shared.Persistence;

public abstract class GenericRepository<T, ID, TContext> : IGenericRepository<T, ID, TContext> where T : class where TContext : DbContext
{
    protected TContext Context { get; }

    protected GenericRepository(TContext context)
    {
        Context = context;

        CurrentDbSet = Context.Set<T>();
    }

    protected DbSet<T> CurrentDbSet { get; set; }

    public async Task<EntityEntry<T>> AddAsync(
        T entity
    )
    {
        return await CurrentDbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(
        IEnumerable<T> entities
    )
    {
        await CurrentDbSet.AddRangeAsync(entities);
    }

    public Task<EntityEntry<T>> Update(
        T entity
    )
    {
        var update = Context.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(update);
    }

    public void Remove(
        T entity
    )
    {
        CurrentDbSet.Remove(entity);
    }

    public void RemoveRange(
        IEnumerable<T> entities
    )
    {
        CurrentDbSet.RemoveRange(entities);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(
        ID id
    )
    {
        return await CurrentDbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetByIdsAsync(
        IEnumerable<ID> ids
    )
    {
        return await CurrentDbSet.FindByIdsAsync(ids);
    }
}