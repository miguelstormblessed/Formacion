using FluentAssertions;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.Responses;

public class UserResponseCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string name = UserNameMother.CreateRandom().Name;
        string email = UserEmailMother.CreateRandom().Email;
        string registrationNumber = VehicleRegistrationMother.CreateRandom().RegistrationValue;
        string color = VehicleColorMother.CreateRandom().Value.ToString();
        // WHEN
        UserResponseCtrl response = UserResponseCtrl.Create(name, email, registrationNumber, color);
        // THEN
        response.Name.Should().Be(name);
        response.Email.Should().Be(email);
        response.RegistrationVehicleNumber.Should().Be(registrationNumber);
        response.VehicleColor.Should().Be(color);
    }
    
}