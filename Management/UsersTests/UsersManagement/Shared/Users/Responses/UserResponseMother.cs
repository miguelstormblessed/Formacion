using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.Responses;

public class UserResponseMother
{
    public static UserResponse CreateRandom()
    {
        return UserResponse.Create(
                Guid.NewGuid().ToString(),
                UserNameMother.CreateRandom().Name,
                UserEmailMother.CreateRandom().Email,
                UserStateMother.CreateRandom().Active
                );
    }
}