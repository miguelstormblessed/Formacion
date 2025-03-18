using UsersManagement.Shared.Vehicles.Domain.Exceptions;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Application.Find;

public class VehicleFinder
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleFinder(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public Vehicle? Execute(VehicleId vehicleId)
    {
        Vehicle vehicle = this._vehicleRepository.GetById(vehicleId);
        if (vehicle is null)
        {
            throw new VehicleNotFoundException();
        }
        return vehicle;
    }
}