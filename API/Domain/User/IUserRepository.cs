namespace API.Domain.User;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> List();
    Task<UserEntity?> SearchByUsername(string userName);
}
