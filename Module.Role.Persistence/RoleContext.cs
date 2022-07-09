using Microsoft.EntityFrameworkCore;

namespace Module.Role.Persistence;

public class RoleContext:DbContext
{
    public DbSet<Domain.Role> Roles { get; set; } = null!;
}