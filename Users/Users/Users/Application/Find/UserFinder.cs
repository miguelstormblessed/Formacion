using Users.Shared.Users.Domain.Exceptions;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;

namespace Users.Users.Application.Find;

public class UserFinder
{
    private readonly IUserRepository _userRepository;

    public UserFinder(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Usuario Execute(UserId userId)
    {
        //var user2 = Usuario.CreateUserActivated("","", "", VehicleResponse.Create("", ))
        //var userId2 = UserId.Create(Guid.NewGuid().ToString());
        var result = _userRepository.Find(userId);
        if (result is null)
        {
            throw new UserNotFoundException();
        }

        return result;
    }
}