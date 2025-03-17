using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Vehicles.Domain.Requests;

public class VehicleRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        string registrationNumber = VehicleRegistrationMother.CreateRandom().RegistrationValue;
        string vehicleColor = VehicleColorMother.CreateRandom().Value.ToString();
        // WHEN
        VehicleRequest request = new VehicleRequest(id, registrationNumber, vehicleColor);
        // THEN
        request.Id.Should().Be(id);
        request.RegistrationNumber.Should().Be(registrationNumber);
        request.VehicleColor.Should().Be(vehicleColor);
    }
}