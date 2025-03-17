using UsersManagement.Shared.Vehicles.Domain.Commands;
using UsersManagement.Vehicles.Application.Create;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Vehicles.Application.Create;

public class VehicleCreatorCommandHandlerTest : VehiclesModuleApplicationUnitTestCase 
{
    private readonly VehicleCreatorCommandHandler _handler;

    public VehicleCreatorCommandHandlerTest()
    {
        VehicleCreator vehicleCreator = new VehicleCreator(this._vehicleRepositoryMock.Object);
        _handler = new VehicleCreatorCommandHandler(vehicleCreator);
    }
    [Fact]
    public async Task ShouldCallSaveVehicle()
    {
        // GIVEN
        VehicleCreatorCommand creatorCommand = VehicleCreatorCommand.Create(Guid.NewGuid().ToString());
        Vehicle vehicle = Vehicle.Create(
            VehicleId.Create(creatorCommand.VehicleId),
            VehicleRegistration.Create(creatorCommand.VehicleRegistrationNumber),
            VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Red)
        );
        // WHEN
        await this._handler.HandleAsync(creatorCommand);
        // THEN
        this.ShouldHaveCalledCreateVehicleWithCorrectParametersOnce(vehicle);
        
    }
    
}