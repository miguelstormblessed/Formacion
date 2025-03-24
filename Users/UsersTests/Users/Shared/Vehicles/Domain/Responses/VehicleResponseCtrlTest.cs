using FluentAssertions;
using Users.Shared.Vehicles.Domain.Responses;

namespace UsersTests.Users.Shared.Vehicles.Domain.Responses;

public class VehicleResponseCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string color = VehicleResponseMother.CreateRandom().VehicleColor;
        string number = VehicleResponseMother.CreateRandom().VehicleRegistration;
        // WHEN
        VehicleResponseCtrl response = new VehicleResponseCtrl(color, number);
        // THEN
        response.VehicleColor.Should().Be(color);
        response.VehicleRegistration.Should().Be(number);
    }
}