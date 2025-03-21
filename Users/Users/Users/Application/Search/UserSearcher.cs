using Users.Users.Domain;

namespace Users.Users.Application.Search;

public class UserSearcher
{
    private readonly IUserRepository _userRepository;

    public UserSearcher(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Usuario>> ExecuteAsync()
    {
        return await _userRepository.SearchUsers();
    }
}