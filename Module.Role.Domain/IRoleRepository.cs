using Microsoft.EntityFrameworkCore;
using Module.Shared.Domain;

namespace Module.Role.Domain;

public interface IRoleRepository<TDbContext> : IGenericRepository<Role, int, TDbContext> where TDbContext : DbContext
{
    IEnumerable<Role> SearchByName(
        string name
    );
}