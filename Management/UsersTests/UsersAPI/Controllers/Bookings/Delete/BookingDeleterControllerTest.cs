using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Shared.Users.Requests;
using UsersTests.UsersManagement.Shared.Users.Responses;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersAPI.Controllers.Bookings.Delete;
[Collection("Tests collection")]
public class BookingDeleterControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturn200Ok()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponse.Create(
            vehicle.Id.IdValue,
            vehicle.VehicleRegistration.RegistrationValue,
            vehicle.VehicleColor.Value.ToString()
            );
        Usuario user = UserMother.CreateRandom();
        user.Vehicle = vehicleResponse;
        UserResponse userResponse = UserResponse.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            user.State.Active
        );
        Booking booking = Booking.Create(
            BookingIdMother.CreateRandom(),
            BookingDateMother.CreateRandom(),
            vehicleResponse,
            userResponse
        );
        UserRequest userRequest = new UserRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.Id.IdValue
            );
        
        BookingRequestCtrl bookingRequestCtrl = new BookingRequestCtrl(
            booking.Id.IdValue,
            booking.Date.DateValue,
            booking.VehicleResponse.Id,
            booking.UserResponse.Id);
        // WHEN
        HttpResponseMessage userHttpResponse = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", userRequest);
        HttpResponseMessage bookingHttpResponse = await this.HttpClient.PostAsJsonAsync("/BookingCreator", bookingRequestCtrl);
        HttpResponseMessage response = await this.HttpClient.DeleteAsync($"/BookingDeleter?id={booking.Id.IdValue}");
        
        // THEN
        userHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        bookingHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldReturn400BadRequest_WhenInvalidIdExceptionIsThrown()
    {
        // GIVEN
        string id = "añlsdkfj";
        // WHEN
        HttpResponseMessage response = await this.HttpClient.DeleteAsync($"/BookingDeleter?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}