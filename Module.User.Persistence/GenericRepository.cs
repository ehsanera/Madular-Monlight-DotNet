using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Module.Shared.Domain;

namespace Module.User.Persistence;

public abstract class GenericRepository<T, ID> : IGenericRepository<T, ID> where T : class
{
    protected GenericRepository(UserContext context)
    {
        Context = context;
    }

    protected UserContext Context { get; }

    public async Task<EntityEntry<T>> AddAsync(T entity)
    {
        return await Context.Set<T>().AddAsync(entity);
    }

    public Task<EntityEntry<T>> Update(T entity)
    {
        var update = Context.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(update);
    }

    public async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(ID id)
    {
        return await Context.Set<T>().FindAsync(id);
    }
}