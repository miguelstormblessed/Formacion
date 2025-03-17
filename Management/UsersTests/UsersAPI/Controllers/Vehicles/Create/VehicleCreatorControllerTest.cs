using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersAPI.Controllers.Vehicles.Create;
[Collection("Tests collection")]
public class VehicleCreatorControllerTest: ApiTestCase
{
    [Fact]
    public async Task ShouldReturnInvalidIdException() 
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest request = new VehicleRequest(
            "abc",
            vehicle.VehicleRegistration.RegistrationValue,
            vehicle.VehicleColor.ToString());
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnInvalidVehicleRegistrationNumberException()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest request = new VehicleRequest(
            vehicle.Id.IdValue,
            vehicle.VehicleRegistration.RegistrationValue.Replace("-",""),
            vehicle.VehicleColor.ToString());
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnExceptionWhenInvalidColor()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest request = new VehicleRequest(
            vehicle.Id.IdValue,
            vehicle.VehicleRegistration.RegistrationValue,
            "DALKDFJ");
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task ShouldReturnCreatedVehicle()
    {
        // GIVEN
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest request = new VehicleRequest(
            vehicle.Id.IdValue,
            vehicle.VehicleRegistration.RegistrationValue,
            vehicle.VehicleColor.ToString());
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}