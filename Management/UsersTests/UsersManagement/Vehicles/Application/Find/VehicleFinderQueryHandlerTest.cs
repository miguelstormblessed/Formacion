using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Vehicles.Application.Find;

public class VehicleFinderQueryHandlerTest : VehiclesModuleApplicationUnitTestCase
{
    private readonly VehicleFinderQueryHandler _handler;

    public VehicleFinderQueryHandlerTest()
    {
        VehicleFinder finder = new VehicleFinder(this._vehicleRepositoryMock.Object);
        _handler = new VehicleFinderQueryHandler(finder);
    }
    [Fact]
    public async Task ShouldReturnAVehicleResponse()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldGetByIdVehicle(vehicle, vehicle.Id);
        VehicleFinderQuery query = VehicleFinderQuery.Create(vehicle.Id.IdValue);
        // WHEN
        VehicleResponse result = await this._handler.HandleAsync(query);
        // THEN
        result.Id.Should().Be(vehicle.Id.IdValue);
        result.VehicleRegistration.Should().Be(vehicle.VehicleRegistration.RegistrationValue);
        result.VehicleColor.Should().Be(vehicle.VehicleColor.Value.ToString());
    }
}