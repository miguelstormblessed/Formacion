using FluentAssertions;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersAPI.Configuration;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Vehicles.Infrastructure;
public class VehicleRepositoryTest : VehicleModuleInfrastructureTestCase
{
    [Fact]
    public void ShouldSaveAVehicle()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        // WHEN
        this.VehicleRepository.Save(vehicle);
        Vehicle? result = this.VehicleRepository.GetById(vehicle.Id);
        // THEN
        result.Should().BeEquivalentTo(vehicle);
        
    }
    
    [Fact]
    public void ShouldGetById()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.VehicleRepository.Save(vehicle);
        // WHEN
        Vehicle result = this.VehicleRepository.GetById(vehicle.Id);
        // THEN
        result.Id.IdValue.Should().Be(vehicle.Id.IdValue);
        result.VehicleRegistration.RegistrationValue.Should().Be(vehicle.VehicleRegistration.RegistrationValue);
        result.VehicleColor.Value.ToString().Should().Be(vehicle.VehicleColor.Value.ToString());
    }

    [Fact]
    public async Task SholdSearchVehicles()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.VehicleRepository.Save(vehicle);
        // WHEN
        IEnumerable<Vehicle> result = await this.VehicleRepository.Search();
        // THEN
        result.Count().Should().BeGreaterThanOrEqualTo(1);
    }
}