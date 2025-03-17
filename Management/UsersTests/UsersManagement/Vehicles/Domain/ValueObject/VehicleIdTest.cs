using FluentAssertions;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

public class VehicleIdTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        VehicleId vehicleId = VehicleId.Create(id);
        // THEN
        vehicleId.IdValue.Should().Be(id);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        VehicleId vehicleId = VehicleIdMother.CreateRandom();
        // WHEN
        VehicleId vehicleId2 = VehicleId.Create(vehicleId.IdValue);
        VehicleId vehicleId3 = VehicleId.Create(vehicleId.IdValue);
        // THEN
        vehicleId2.Should().Be(vehicleId3);
    }
}