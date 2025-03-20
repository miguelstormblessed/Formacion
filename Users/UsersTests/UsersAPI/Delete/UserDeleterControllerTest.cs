using System.Net;
using System.Net.Http.Json;
using Bogus.DataSets;
using FluentAssertions;
using Newtonsoft.Json;
using Users.Shared.Users.Domain.Requests;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;

namespace UsersTests.UsersAPI.Delete;
[Collection("Tests collection")]
public class UserDeleterControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenInvalidIdExceptionIsThrown()
    {
        // GIVEN
        string id = "123";
        // WHEN
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/UserDeleter?id={id}");
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
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/UserDeleter?id={id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        //response.Content.ReadAsStringAsync().Result.Should().Contain("User not found");
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenUserIsAlreadyDeleterExceptionIsThrown()
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
        
        HttpResponseMessage responseCreate = await this.HttpClient.PostAsJsonAsync("/UserInactiveCreator", request);
        
        // THEN
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/UserDeleter?id={user.Id.Id}");
        // WHEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Contain("User is already deleted");
        
    }

    [Fact]
    public async Task ShouldReturn200OK()
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
        
        HttpResponseMessage responseCreate = await this.HttpClient.PostAsJsonAsync("/UserActiveCreator", request);
        // WHEN
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/UserDeleter?id={user.Id.Id}");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}