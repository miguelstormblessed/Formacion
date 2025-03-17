using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersAPI.Controllers.Vehicles.Find;
[Collection("Tests collection")]
public class VehicleFinderControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnVehicleNotFoundException()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/VehicleFinder?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task ShouldReturnInvalidIdException()
    {
        // GIVEN
        string id = "abc";
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/VehicleFinder?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn200Ok()
    {
        Vehicle vehicle = VehicleMother.CreateRandom();
        VehicleRequest request = new VehicleRequest(
            vehicle.Id.IdValue,
            vehicle.VehicleRegistration.RegistrationValue,
            vehicle.VehicleColor.ToString());
        HttpResponseMessage createdResponse = await this.HttpClient.PostAsJsonAsync("/VehicleCreator", request);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/VehicleFinder?id={vehicle.Id.IdValue}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}