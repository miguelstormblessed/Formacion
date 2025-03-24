using FluentAssertions;
using Users.Shared.Users.Domain.Requests;
using UsersTests.Users.Shared.Users.Responses;

namespace UsersTests.Users.Shared.Users.Requests;

public class UserUpdaterRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        string name = UserResponseMother.CreateRandom().Name;
        string email = UserResponseMother.CreateRandom().Email;
        string vehicleId = Guid.NewGuid().ToString();
        bool state = UserResponseMother.CreateRandom().State;
        // WHEN
        UserUpdaterRequest request = new UserUpdaterRequest(id, name, email, state, vehicleId);
        // THEN
        request.Id.Should().Be(id);
        request.Name.Should().Be(name);
        request.Email.Should().Be(email);
        request.State.Should().Be(state);
        request.VehicleId.Should().Be(vehicleId);
    }
}