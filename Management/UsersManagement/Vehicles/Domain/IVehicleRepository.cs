using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Domain;

public interface IVehicleRepository
{
    public void Save(Vehicle vehicle);
    
    public Vehicle GetById(VehicleId vehicleId);
    
    public Task<IEnumerable<Vehicle>> Search();
}