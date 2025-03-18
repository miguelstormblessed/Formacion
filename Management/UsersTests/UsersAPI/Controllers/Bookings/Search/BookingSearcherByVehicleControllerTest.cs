using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Shared.Users.Responses;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersAPI.Controllers.Bookings.Search;
[Collection("Tests collection")]
public class BookingSearcherByVehicleControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturn400BadRequest_WhenInvalidIdException()
    {
        // GIVEN
        string id = "ñalk3kdk4";
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/BookingSearcherbyVehicle?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn404NotFound_WhenUserIsNotFound()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/BookingSearcherbyVehicle?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task ShouldReturn200Ok_WhenUserIsFound()
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
            .Setup(h => h.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(mockResponse);
        
        // WHEN
        HttpResponseMessage bookingHttpResponse = await this.HttpClient.PostAsJsonAsync("/BookingCreator", requestCtrl);
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/BookingSearcherByVehicle?id={vehicleId}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}