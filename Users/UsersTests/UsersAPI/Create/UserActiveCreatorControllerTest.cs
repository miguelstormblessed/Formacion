using System.Net;
using System.Net.Http.Json;
using Bogus.DataSets;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Users.Shared.Users.Domain.Requests;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;

namespace UsersTests.UsersAPI.Create;
[Collection("Tests collection")]
public class UserActiveCreatorControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenRequestIsNull()
    {
        // GIVEN
        UserRequest request = null;
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenExceptionTrhown()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        UserRequest request = new UserRequest(
            "dkaldkfj",
            user.Name.Name,
            user.Email.Email,
            vehicle.Id);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task ShouldReturnCreated()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        // Existing vehicle in bbdd
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        UserRequest request = new UserRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.Id);
        HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
        mockResponse.Content = new StringContent(JsonConvert.SerializeObject(vehicle));

        this.ShouldFindVehicleByHttp(mockResponse);
        // WHEN
        
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}