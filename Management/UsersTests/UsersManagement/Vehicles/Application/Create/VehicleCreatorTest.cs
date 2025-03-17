using UsersManagement.Vehicles.Application.Create;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Vehicles.Application.Create;

public class VehicleCreatorTest : VehiclesModuleApplicationUnitTestCase
{
    private readonly VehicleCreator _vehicleCreator;

    public VehicleCreatorTest()
    {
        this._vehicleCreator = new VehicleCreator(this._vehicleRepositoryMock.Object);
    }
    [Fact]
    public void ShouldCallCreateVehicleWithCorrectParametersOnce()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldCreateNewVehicle(vehicle);
        // WHEN
        _vehicleCreator.Execute(vehicle.Id, vehicle.VehicleRegistration, vehicle.VehicleColor);
        // THEN
        this.ShouldHaveCalledCreateVehicleWithCorrectParametersOnce(vehicle);
    }
}