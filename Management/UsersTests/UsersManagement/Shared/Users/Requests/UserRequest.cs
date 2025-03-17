using FluentAssertions;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.Requests;

public class UserRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        string name = UserNameMother.CreateRandom().Name;
        string email = UserEmailMother.CreateRandom().Email;
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