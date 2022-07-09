using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Module.Shared.Domain;

public interface IGenericRepository<T, ID, TContext> where T : class where TContext : DbContext
{
    Task<EntityEntry<T>> AddAsync(
        T entity
    );

    Task AddRangeAsync(
        IEnumerable<T> entities
    );

    Task<EntityEntry<T>> Update(
        T entity
    );

    void Remove(
        T entity
    );

    void RemoveRange(
        IEnumerable<T> entities
    );

    Task<int> SaveChangesAsync();

    Task<T?> GetByIdAsync(
        ID id
    );

    Task<IEnumerable<T>> GetByIdsAsync(
        IEnumerable<ID> ids
    );

    IQueryable<T> GetAll();
}