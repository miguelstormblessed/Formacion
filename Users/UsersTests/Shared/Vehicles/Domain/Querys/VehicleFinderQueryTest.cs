using FluentAssertions;
using Users.Shared.Vehicles.Domain.Querys;

namespace UsersTests.Shared.Vehicles.Domain.Querys;

public class VehicleFinderQueryTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(id);
        // THEN
        vehicleFinderQuery.VehicleId.Should().Be(id);
    }
    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(Guid.NewGuid().ToString());
        // WHEN
        VehicleFinderQuery vehicleFinderQuery2 = VehicleFinderQuery.Create(vehicleFinderQuery.VehicleId);
        VehicleFinderQuery vehicleFinderQuery3 = VehicleFinderQuery.Create(vehicleFinderQuery.VehicleId);
        // THEN
        vehicleFinderQuery2.Should().BeEquivalentTo(vehicleFinderQuery3);
    }
}