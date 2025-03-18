using FluentAssertions;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.Responses;

public class UserResponseCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string name = UserResponseMother.CreateRandom().Name;
        string email = UserResponseMother.CreateRandom().Email;
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