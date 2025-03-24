using FluentAssertions;
using Users.Shared.Vehicles.Domain.Requests;
using UsersTests.Users.Shared.Vehicles.Domain.Responses;

namespace UsersTests.Users.Shared.Vehicles.Domain.Requests;

public class VehicleRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        string registrationNumber = VehicleResponseMother.CreateRandom().VehicleRegistration;
        string vehicleColor = VehicleResponseMother.CreateRandom().VehicleColor;
        // WHEN
        VehicleRequest request = new VehicleRequest(id, registrationNumber, vehicleColor);
        // THEN
        request.Id.Should().Be(id);
        request.RegistrationNumber.Should().Be(registrationNumber);
        request.VehicleColor.Should().Be(vehicleColor);
    }
}