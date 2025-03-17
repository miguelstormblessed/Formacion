using System.Runtime.CompilerServices;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersAPI.Configuration;

namespace UsersTests.UsersManagement.Vehicles.Infrastructure;

public class VehicleModuleInfrastructureTestCase : InfraestructureTestCase<Program>
{
    protected IVehicleRepository VehicleRepository => this.GetService<IVehicleRepository>();
}