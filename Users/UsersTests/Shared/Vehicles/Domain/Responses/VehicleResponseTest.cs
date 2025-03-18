using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;

public class VehicleResponseTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        // WHEN
        VehicleResponse result = VehicleResponse.Create(
            vehicleResponse.Id,
            vehicleResponse.VehicleRegistration,
            vehicleResponse.VehicleColor
            );
        // THEN
        result.Id.Should().Be(vehicleResponse.Id);
        result.VehicleRegistration.Should().Be(vehicleResponse.VehicleRegistration);
        result.VehicleColor.Should().Be(vehicleResponse.VehicleColor);
    }
}