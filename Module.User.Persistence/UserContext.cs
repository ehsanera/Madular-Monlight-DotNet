using Microsoft.EntityFrameworkCore;
using Module.Shared.Domain;

namespace Module.User.Persistence;

public class UserContext : DbContext, IDbContext
{
    public DbSet<Domain.User> Users { get; set; }
}