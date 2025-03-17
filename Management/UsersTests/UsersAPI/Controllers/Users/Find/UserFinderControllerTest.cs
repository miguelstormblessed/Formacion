using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;

namespace UsersTests.UsersAPI.Controllers.Users.Find;
[Collection("Tests collection")]
public class UserFinderControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenInvalidIdExceptionIsThrown()
    {
        // GIVEN
        string id = "123";
        // WHEN
        HttpResponseMessage response = await HttpClient.GetAsync($"/UserFinder?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Contain("Invalid Id");
    }
    
    [Fact]
    public async Task ShouldReturnBadRequest_WhenUserNotFoundExceptionIsThrown()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        HttpResponseMessage response = await HttpClient.GetAsync($"/UserFinder?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.ReadAsStringAsync().Result.Should().Contain("User not found");
    }

    [Fact]
    public async Task ShouldReturnOk_WhenAllIsOk()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"),VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserRequest request = new UserRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.Id.IdValue);
        HttpResponseMessage responseCreate = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", request);
        // WHEN
        HttpResponseMessage response = await HttpClient.GetAsync($"/UserFinder?id={user.Id.Id}");
        // THEN
        responseCreate.StatusCode.Should().Be(HttpStatusCode.Created);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
 
}