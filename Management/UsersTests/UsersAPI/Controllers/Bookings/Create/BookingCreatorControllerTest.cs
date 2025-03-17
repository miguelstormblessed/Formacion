using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersAPI.Controllers.Bookings.Create;
[Collection("Tests collection")]
public class BookingCreatorControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenInvalidIdException()
    {
        // GIVEN
        string id = "123";
        Booking booking = BookingMother.CreateRandom();
        BookingRequestCtrl requestCtrl = new BookingRequestCtrl(
            id,
            booking.Date.DateValue,
            booking.VehicleResponse.Id,
            booking.UserResponse.Id);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenInvalidDateFormatException()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        BookingRequestCtrl requestCtrl = new BookingRequestCtrl(
            booking.Id.IdValue,
            "asdf",
            booking.VehicleResponse.Id,
            booking.UserResponse.Id);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenVehicleNotFoundException()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        BookingRequestCtrl requestCtrl = new BookingRequestCtrl(
            booking.Id.IdValue,
            booking.Date.DateValue,
            booking.VehicleResponse.Id,
            booking.UserResponse.Id);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnBadRquest_WhenUserNotFoundException()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest vehicleRequest = new VehicleRequest(vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue,
            vehicle.VehicleColor.Value.ToString());
        HttpResponseMessage vehicleResponse = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", vehicleRequest);
            
        BookingRequestCtrl requestCtrl = new BookingRequestCtrl(
            Guid.NewGuid().ToString(),
            BookingDateMother.CreateRandom().DateValue,
            vehicle.Id.IdValue,
            Guid.NewGuid().ToString());
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        // THEN
        vehicleResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnCreated201()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest vehicleRequest = new VehicleRequest(vehicle.Id.IdValue, vehicle.VehicleRegistration.RegistrationValue,
            vehicle.VehicleColor.Value.ToString());
        HttpResponseMessage vehicleResponse = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", vehicleRequest);

        Usuario user = UserMother.CreateRandom();
        UserRequest userRequest = new UserRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.Id.IdValue
        );
        HttpResponseMessage userResponse = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", userRequest);

        BookingRequestCtrl requestCtrl = new BookingRequestCtrl(
            Guid.NewGuid().ToString(),
            BookingDateMother.CreateRandom().DateValue,
            vehicle.Id.IdValue,
            user.Id.Id);
        
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        
        // THEN
        vehicleResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        userResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    

}