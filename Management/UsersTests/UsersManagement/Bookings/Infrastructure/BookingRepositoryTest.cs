using FluentAssertions;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Bookings.Infrastructure;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersAPI.Configuration;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Bookings.Infrastructure;
public class BookingRepositoryTest : VehicleModuleInfrastructureTestCase
{
    [Fact]
    public void ShouldSaveBookings()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        
        Usuario user = UserMother.CreateRandom();
        booking.UserResponse = UserResponse.Create(
            user.Id.Id, user.Name.Name, user.Email.Email, user.State.Active);
        this.UserRepository.Save(user);
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
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
    public void ShouldDeleteBookings()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        
        Usuario user = UserMother.CreateRandom();
        booking.UserResponse = UserResponse.Create(
            user.Id.Id, user.Name.Name, user.Email.Email, user.State.Active);
        this.UserRepository.Save(user);
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        this.BookingRepository.Save(booking);
        // WHEN
        this.BookingRepository.Delete(booking.Id);
        // THEN
        Booking result = this.BookingRepository.GetBookingById(booking.Id);
        result.Should().BeNull();
    }

    [Fact]
    public void ShouldGetByBookingId()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        
        Usuario user = UserMother.CreateRandom();
        booking.UserResponse = UserResponse.Create(
            user.Id.Id, user.Name.Name, user.Email.Email, user.State.Active);
        this.UserRepository.Save(user);
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
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
        
        Usuario user = UserMother.CreateRandom();
        booking.UserResponse = UserResponse.Create(
            user.Id.Id, user.Name.Name, user.Email.Email, user.State.Active);
        this.UserRepository.Save(user);
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        booking.VehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        this.VehicleRepository.Save(vehicle);
        
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
        
        Usuario user = UserMother.CreateRandom();
        this.UserRepository.Save(user);
        UserResponse response = UserResponse.Create(
            user.Id.Id, user.Name.Name, user.Email.Email, user.State.Active);
        
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.VehicleRepository.Save(vehicle);
        VehicleResponse vehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        
        booking.UserResponse = response;
        booking.VehicleResponse = vehicleResponse;
        booking2.UserResponse = response;
        booking2.VehicleResponse = vehicleResponse;
        this.BookingRepository.Save(booking);
        this.BookingRepository.Save(booking2);
        // WHEN
        IEnumerable<Booking> result = await this.BookingRepository.Search(new BookingByUserIdSpecification(user.Id.Id));
        // THEN
        result.Count().Should().Be(2);
    }

    [Fact]
    public async Task ShouldGetAllBookingsByVehicle()
    {
        // GIVEN 
        Booking booking = BookingMother.CreateRandom();
        Booking booking2 = BookingMother.CreateRandom();
        
        Usuario user = UserMother.CreateRandom();
        this.UserRepository.Save(user);
        UserResponse response = UserResponse.Create(
            user.Id.Id, user.Name.Name, user.Email.Email, user.State.Active);
        
        
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.VehicleRepository.Save(vehicle);
        VehicleResponse vehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue, vehicle.VehicleColor.Value.ToString());
        
        booking.UserResponse = response;
        booking.VehicleResponse = vehicleResponse;
        booking2.UserResponse = response;
        booking2.VehicleResponse = vehicleResponse;
        this.BookingRepository.Save(booking);
        this.BookingRepository.Save(booking2);
        // WHEN
        IEnumerable<Booking> result = await this.BookingRepository.Search(new BookingByVehicleIdSpecification(vehicle.Id.IdValue));
        // THEN
        result.Count().Should().Be(2);
    }
    
}