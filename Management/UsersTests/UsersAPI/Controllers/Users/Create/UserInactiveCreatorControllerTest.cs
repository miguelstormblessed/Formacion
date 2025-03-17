using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersAPI.Controllers.Users.Create;
[Collection("Tests collection")]
public class UserInactiveCreatorControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenRequestIsNull()
    {
        // GIVEN
        UserRequest request = null;
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserInactiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenExceptionTrhown()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        UserRequest request = new UserRequest(
            "dkaldkfj",
            user.Name.Name,
            user.Email.Email,
            vehicle.Id.IdValue);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserInactiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task ShouldReturnCreated()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"), VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserRequest request = new UserRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.Id.IdValue);
        // WHEN
        
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserInactiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}