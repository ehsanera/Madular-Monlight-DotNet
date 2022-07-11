using Microsoft.EntityFrameworkCore;
using Module.Shared.Domain;

namespace Module.User.Domain;

public interface IUserRepository<TContext> : IGenericRepository<User, int, TContext> where TContext : DbContext
{
    IEnumerable<User> SearchByName(string search);
}