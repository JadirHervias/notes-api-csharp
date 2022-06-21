using API.Domain.User;

namespace API.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NotesAPIContext _context;

    public UserRepository(NotesAPIContext NotesAPIContext) => _context = NotesAPIContext;

    public Task<IEnumerable<UserEntity>> List()
    {
        var query = from n in _context.Users select n;

        return Task.Run(() => query.AsEnumerable());
    }

    public async Task<UserEntity?> SearchByUsername(string userName)
    {
        IEnumerable<UserEntity> results = from u in _context.Users
                                          where u.UserName.Contains(userName)
                                          select u;

        UserEntity? user = results.First();

        if (null == user)
        {
            return null;
        }

        return await Task.Run(() => user);

    }
}
