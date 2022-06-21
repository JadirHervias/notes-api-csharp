using API.Domain.User;

namespace API.Services;

public class UserService : IUserService
{
    readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserEntity>> List()
    {
        return await _repository.List();
    }

    public async Task<UserEntity?> SearchByUserName(string userName)
    {
        UserEntity? user = await _repository.SearchByUsername(userName);

        return user;
    }
}

public interface IUserService
{
    Task<IEnumerable<UserEntity>> List();
    Task<UserEntity?> SearchByUserName(string userName);
}
