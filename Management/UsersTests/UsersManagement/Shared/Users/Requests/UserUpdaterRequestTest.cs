using FluentAssertions;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.Requests;

public class UserUpdaterRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        string name = UserNameMother.CreateRandom().Name;
        string email = UserEmailMother.CreateRandom().Email;
        string vehicleId = Guid.NewGuid().ToString();
        bool state = UserStateMother.CreateRandom().Active;
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