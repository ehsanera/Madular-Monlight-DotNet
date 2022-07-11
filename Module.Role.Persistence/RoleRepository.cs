using Module.Role.Domain;
using Module.Shared.Persistence;

namespace Module.Role.Persistence;

public class RoleRepository : GenericRepository<Domain.Role, int, RoleContext>,
    IRoleRepository<RoleContext>
{
    public RoleRepository(
        RoleContext context
    ) : base(context) { }

    public IEnumerable<Domain.Role> SearchByName(
        string name
    )
    {
        return CurrentDbSet.Where(r => r.Name.Contains(name));
    }
}