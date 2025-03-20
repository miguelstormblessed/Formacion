using System.Net;
using System.Net.Http.Json;
using Bogus.DataSets;
using FluentAssertions;
using Users.Shared.Users.Domain.Requests;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;

namespace UsersTests.UsersAPI.Create;
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
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        UserRequest request = new UserRequest(
            "dkaldkfj",
            user.Name.Name,
            user.Email.Email,
            vehicle.Id);
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
        VehicleResponse vehicle = VehicleResponse.Create("28d45f10-dbf8-4b1c-99df-d139fa215d80",
            "4vicz-ihq67", "Green");
        UserRequest request = new UserRequest(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.Id);
        // WHEN
        
        HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/UserInactiveCreator", request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}