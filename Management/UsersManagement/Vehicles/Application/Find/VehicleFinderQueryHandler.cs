using Cojali.Shared.Domain.Bus.Query;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Application.Find;

public class VehicleFinderQueryHandler : IQueryHandler<VehicleFinderQuery, VehicleResponse>
{
    private readonly VehicleFinder _vehicleFinder;

    public VehicleFinderQueryHandler(VehicleFinder vehicleFinder)
    {
        this._vehicleFinder = vehicleFinder;
    }

    public async Task<VehicleResponse> HandleAsync(VehicleFinderQuery query)
    {
        Vehicle? vehicle = _vehicleFinder.Execute(VehicleId.Create(query.VehicleId));

        return VehicleResponse.Create(vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
    }
}