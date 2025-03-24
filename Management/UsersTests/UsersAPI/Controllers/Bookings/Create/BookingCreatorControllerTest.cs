using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Shared.Users.Responses;
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
        // EXISTING IN DDBB
        string vehicleId = "28548eac-8829-4275-b336-078e00e96f56";
        string userId = "0babdeec-c946-4042-a2cf-c2b452d5176d";
        
        UserResponse userResponse = UserResponse.Create(
            "0babdeec-c946-4042-a2cf-c2b452d5176d",
            "ñalsdjkf",
            "añlsdf@mail",
            true);
        
        BookingRequestCtrl requestCtrl = new BookingRequestCtrl(
            Guid.NewGuid().ToString(),
            BookingDateMother.CreateRandom().DateValue,
            vehicleId,
            userId);
        
        
        HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
        mockResponse.Content = new StringContent(JsonConvert.SerializeObject(userResponse));
    
        // Configure the mock client service with the specific URL that will be called
        HttpClientService
            .Setup(h => h.GetAsync($"https://localhost:7172/UserFinder?id={userId}"))
            .ReturnsAsync(mockResponse);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    

}