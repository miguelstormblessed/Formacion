using Cojali.Shared.Domain.Bus.Command;
using UsersManagement.Shared.Vehicles.Domain.Commands;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Application.Create;

public class VehicleCreatorCommandHandler : ICommandHandler<VehicleCreatorCommand>
{
    private readonly VehicleCreator _vehicleCreator;

    public VehicleCreatorCommandHandler(VehicleCreator vehicleCreator)
    {
        _vehicleCreator = vehicleCreator;
    }

    public async Task HandleAsync(VehicleCreatorCommand creatorCommand)
    {
        VehicleId vehicleId = VehicleId.Create(creatorCommand.VehicleId);
        VehicleRegistration vehicleRegistration = VehicleRegistration.Create(creatorCommand.VehicleRegistrationNumber);
        VehicleColor vehicleColor = VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Red);
        _vehicleCreator.Execute(vehicleId, vehicleRegistration, vehicleColor);
    }
}