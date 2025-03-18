using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersAPI.Configuration;

namespace UsersTests.UsersManagement.Bookings.Infrastructure;

public class VehicleModuleInfrastructureTestCase : InfraestructureTestCase<Program>
{
    protected IBookingRepository BookingRepository => this.GetService<IBookingRepository>();
    //protected IUserRepository UserRepository => this.GetService<IUserRepository>();
    
    protected IVehicleRepository VehicleRepository => this.GetService<IVehicleRepository>();
    protected IHttpClientService HttpClientService => this.GetService<IHttpClientService>();
}