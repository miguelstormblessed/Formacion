using FluentAssertions;
using Users.Shared.Users.Domain.Responses;

namespace UsersTests.Users.Shared.Users.Responses;

public class UserResponseTest
{
    [Fact]
    public void ShouldIniciatlitePropertiesCorrectly()
    {
        // GIVEN
        UserResponse user = UserResponseMother.CreateRandom();
        // WHEN
        UserResponse result = UserResponse.Create(
            user.Id,
            user.Name,
            user.Email,
            user.State
        );
        // THEN
        user.Id.Should().Be(result.Id);
        user.Name.Should().Be(result.Name);
        user.Email.Should().Be(result.Email);
        user.State.Should().Be(result.State);
    }
}