using System.Net;
using FluentAssertions;

namespace UsersTests.UsersAPI.Controllers.Vehicles.Search;
[Collection("Tests collection")]
public class VehicleSearcherControllerTest : ApiTestCase
{
    [Fact]
    public async Task SearchVehicles()
    {
        // GIVEN
        
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync($"/VehicleSearcher");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}