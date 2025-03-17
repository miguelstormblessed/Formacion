using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Application.Find;

public class VehicleFinderTest : VehiclesModuleApplicationUnitTestCase
{
    private readonly VehicleFinder _vehicleFinder;

    public VehicleFinderTest()
    {
        _vehicleFinder = new VehicleFinder(this._vehicleRepositoryMock.Object);
    }
    [Fact]
    public void ShoudCallGetByIdWithCorrectParametersOnce()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldGetByIdVehicle(vehicle, vehicle.Id);
        // WHEN
        this._vehicleFinder.Execute(vehicle.Id);
        // THEN
        this.ShouldHaveCalledGetByIdWithCorrectParametersOnce(vehicle.Id);
    }

    [Fact]
    public void ShouldThrowVehicleNotFoundException_WhenVehicleNotFound()
    {
        // GIVEN
        VehicleId vehicleId = VehicleIdMother.CreateRandom();
        // WHEN
        var result =  () => this._vehicleFinder.Execute(vehicleId);
        // THEN
        result.Should().Throw<VehicleNotFoundException>();
    }
}