using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersAPI.Controllers.Users.Update;
[Collection("Tests collection")]
public class UserUpdaterControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenInvalidIdExceptionIsThrown()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"), VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserUpdaterRequest request = new UserUpdaterRequest(
            "123",
            user.Name.Name,
            user.Email.Email,
            true,
            vehicle.Id.IdValue);
        // WHEN
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/UserUpdater", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Contain("Invalid Id");
    }
    
    [Fact]
    public async Task ShouldReturnBadRequest_WhenUserNotFoundExceptionIsThrown()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"), VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserUpdaterRequest request = new UserUpdaterRequest(
            user.Id.Id,
            "abc",
            user.Email.Email,
            true,
            vehicle.Id.IdValue);
        // WHEN
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/UserUpdater",request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Contain("Invalid username");
    }
    [Fact]
    public async Task ShouldReturnNotFound_WhenUserNotFoundExceptionIsThrown()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"), VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserUpdaterRequest request = new UserUpdaterRequest(
            Guid.NewGuid().ToString(),
            user.Name.Name,
            user.Email.Email,
            true,
            vehicle.Id.IdValue);
        // WHEN
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/UserUpdater",request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.ReadAsStringAsync().Result.Should().Contain("User not found");
    }
    
    [Fact]
    public async Task ShouldReturnBadRequest_WhenInvalidUserEmailExceptionIsThrown()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"), VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserUpdaterRequest request = new UserUpdaterRequest(
            user.Id.Id,
            user.Name.Name,
            "abc",
            true,
            vehicle.Id.IdValue);
        // WHEN
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/UserUpdater",request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Contain("Invalid email");
    }
    [Fact]
    public async Task ShouldReturnOk_WhenAllIsOk()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = Vehicle.Create(VehicleId.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80"),
            VehicleRegistration.Create("4vicz-ihq67"), VehicleColor.CreateVehicleColor(VehicleColor.ColorValue.Green));
        UserUpdaterRequest request = new UserUpdaterRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            false,
            vehicle.Id.IdValue);
        HttpResponseMessage responseCreate = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", request);
        UserUpdaterRequest requestForUpdate = new UserUpdaterRequest(
            user.Id.Id,
            UserNameMother.CreateRandom().Name,
            UserEmailMother.CreateRandom().Email,
            UserStateMother.CreateRandom().Active,
            vehicle.Id.IdValue);
        // WHEN
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/UserUpdater",requestForUpdate);
        // THEN
        responseCreate.StatusCode.Should().Be(HttpStatusCode.Created);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}