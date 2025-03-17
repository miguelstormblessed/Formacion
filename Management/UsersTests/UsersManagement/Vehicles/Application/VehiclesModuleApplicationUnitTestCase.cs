using Moq;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Application;

public class VehiclesModuleApplicationUnitTestCase
{
    protected VehiclesModuleApplicationUnitTestCase()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
    }
    protected readonly Mock<IVehicleRepository> _vehicleRepositoryMock;



    protected void ShouldCreateNewVehicle(Vehicle vehicle)
    {
        this._vehicleRepositoryMock.Setup(x => x.Save(vehicle));
    }

    protected void ShouldGetByIdVehicle(Vehicle vehicle, VehicleId vehicleId)
    {
        this._vehicleRepositoryMock.Setup(x => x.GetById(vehicleId)).Returns(vehicle);
    }

    protected async Task ShouldSearchVehicles(List<Vehicle> vehicles)
    {
        this._vehicleRepositoryMock.Setup(x => x.Search()).ReturnsAsync(vehicles);
    }
    protected void ShouldHaveCalledCreateVehicleWithCorrectParametersOnce(Vehicle vehicle)
    {
        this._vehicleRepositoryMock.Verify(repo => repo.Save(vehicle), Times.Once);
    }

    protected void ShouldHaveCalledGetByIdWithCorrectParametersOnce(VehicleId vehicleId)
    {
        this._vehicleRepositoryMock.Verify(repo => repo.GetById(vehicleId));
    }

    protected async Task ShouldHaveCalledSearchWithCorrectParametersOnce()
    {
        this._vehicleRepositoryMock.Verify(repo => repo.Search(), Times.Once);
    }
}