using FluentAssertions;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Bookings.Infrastructure;
public class BookingRepositoryTest : VehicleModuleInfrastructureTestCase
{
    

    [Fact]
    public async Task ShouldSaveBookings()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
        // EXISTING IN DDBB
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        booking.UserResponse = userResponse;
        // WHEN
        this.BookingRepository.Save(booking);
        // THEN
        Booking bookingdb = this.BookingRepository.GetBookingById(booking.Id);
        bookingdb.Id.Should().Be(booking.Id);
        bookingdb.Date.Should().Be(booking.Date);
        bookingdb.Status.Should().Be(booking.Status);
        bookingdb.VehicleResponse.Should().Be(booking.VehicleResponse);
        bookingdb.UserResponse.Should().Be(booking.UserResponse);
    }

    [Fact]
    public async Task ShouldDeleteBookings()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
        // EXISTING IN DDBB
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        booking.UserResponse = userResponse;
        this.BookingRepository.Save(booking);
        // WHEN
        this.BookingRepository.Delete(booking.Id);
        // THEN
        Booking result = this.BookingRepository.GetBookingById(booking.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetByBookingId()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
        // EXISTING IN DDBB
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        booking.UserResponse = userResponse;
        
        this.BookingRepository.Save(booking);
        // WHEN
        Booking result = this.BookingRepository.GetBookingById(booking.Id);
        // THEN
        result.Should().Be(booking);
    }
    
    [Fact]
    public async Task ShouldGetAllBookings()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
        // EXISTING IN DDBB
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        booking.UserResponse = userResponse;
        
        this.BookingRepository.Save(booking);
        
        // WHEN
        IEnumerable<Booking> allBookings = await this.BookingRepository.SearchAll();
        // THEN
        allBookings.Count().Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task ShouldGetAllBookingsByUser()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        Booking booking2 = BookingMother.CreateRandom();
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
        // EXISTING IN DDBB
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        
        booking.UserResponse = userResponse;
        booking.VehicleResponse = vehicleResponse;
        booking2.UserResponse = userResponse;
        booking2.VehicleResponse = vehicleResponse;
        this.BookingRepository.Save(booking);
        this.BookingRepository.Save(booking2);
        // WHEN
        IEnumerable<Booking> result = await this.BookingRepository.Search(new BookingByUserIdSpecification(userResponse.Id));
        // THEN
        result.Count().Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task ShouldGetAllBookingsByVehicle()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        Booking booking2 = BookingMother.CreateRandom();
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
        // EXISTING IN DDBB
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        
        booking.UserResponse = userResponse;
        booking.VehicleResponse = vehicleResponse;
        booking2.UserResponse = userResponse;
        booking2.VehicleResponse = vehicleResponse;
        this.BookingRepository.Save(booking);
        this.BookingRepository.Save(booking2);
        // WHEN
        IEnumerable<Booking> result = await this.BookingRepository.Search(new BookingByVehicleIdSpecification(vehicle.Id.IdValue));
        // THEN
        result.Count().Should().Be(2);
    }
    
}