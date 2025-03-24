using FluentAssertions;
using Users.Shared.Users.Domain.Requests;
using UsersTests.Users.Shared.Users.Responses;

namespace UsersTests.Users.Shared.Users.Requests;

public class UserRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        string name = UserResponseMother.CreateRandom().Name;
        string email = UserResponseMother.CreateRandom().Email;
        string vehicleId = Guid.NewGuid().ToString();
        // WHEN
        UserRequest request = new UserRequest(id, name, email, vehicleId);
        // THEN
        request.Id.Should().Be(id);
        request.Name.Should().Be(name);
        request.Email.Should().Be(email);
        request.VehicleId.Should().Be(vehicleId);
    }
}