using UsersManagement.Vehicles.Domain;

namespace UsersManagement.Vehicles.Application.Search;

public class VehicleSearcher
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleSearcher(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IEnumerable<Vehicle>> Execute()
    {
        return await _vehicleRepository.Search();
    }
}