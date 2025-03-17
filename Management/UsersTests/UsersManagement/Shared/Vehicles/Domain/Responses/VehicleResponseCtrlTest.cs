using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;

public class VehicleResponseCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string color = VehicleColorMother.CreateRandom().Value.ToString();
        string number = VehicleRegistrationMother.CreateRandom().RegistrationValue;
        // WHEN
        VehicleResponseCtrl response = new VehicleResponseCtrl(color, number);
        // THEN
        response.Color.Should().Be(color);
        response.RegistrationNumber.Should().Be(number);
    }
}