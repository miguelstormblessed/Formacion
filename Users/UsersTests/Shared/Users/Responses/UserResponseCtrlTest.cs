using FluentAssertions;
using Users.Shared.Users.Domain.Responses;
using UsersTests.Shared.Vehicles.Domain.Responses;

namespace UsersTests.Shared.Users.Responses;

public class UserResponseCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string name = UserResponseMother.CreateRandom().Name;
        string email = UserResponseMother.CreateRandom().Email;
        string registrationNumber = VehicleResponseMother.CreateRandom().VehicleRegistration;
        string color = VehicleResponseMother.CreateRandom().VehicleColor;
        // WHEN
        UserResponseCtrl response = UserResponseCtrl.Create(name, email, registrationNumber, color);
        // THEN
        response.Name.Should().Be(name);
        response.Email.Should().Be(email);
        response.RegistrationVehicleNumber.Should().Be(registrationNumber);
        response.VehicleColor.Should().Be(color);
    }
    
}