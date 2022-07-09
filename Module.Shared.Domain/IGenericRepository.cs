using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Module.Shared.Domain;

public interface IGenericRepository<T, ID> where T : class
{
    Task<EntityEntry<T>> AddAsync(T entity);
    Task<EntityEntry<T>> Update(T entity);
    Task<int> SaveAsync();
    Task<T?> GetByIdAsync(ID id);
}