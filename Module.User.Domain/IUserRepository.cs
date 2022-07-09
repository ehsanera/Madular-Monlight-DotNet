using Module.Shared.Domain;

namespace Module.User.Domain;

public interface IUserRepository : IGenericRepository<User, int>
{
    IEnumerable<User> SearchByName(string search);
}