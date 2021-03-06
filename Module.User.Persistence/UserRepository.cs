using Microsoft.EntityFrameworkCore.ChangeTracking;
using Module.Shared.Persistence;
using Module.User.Domain;

namespace Module.User.Persistence;

public class UserRepository : GenericRepository<Domain.User, int, UserContext>, IUserRepository<UserContext>
{
    public UserRepository(
        UserContext context
    ) : base(context)
    {
    }

    public IEnumerable<Domain.User> SearchByName(string search)
    {
        return Context.Users.Where(i => i.Name.Contains(search));
    }
}