using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Application.Create;

public class VehicleCreator
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleCreator(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public void Execute(VehicleId id , VehicleRegistration vehicleRegistration, VehicleColor colorValue)
    {
        Vehicle vehicle = Vehicle.Create(id, vehicleRegistration, colorValue);
        this._vehicleRepository.Save(vehicle);
    }
}