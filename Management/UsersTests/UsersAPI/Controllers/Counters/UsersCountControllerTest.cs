using System.Net;
using FluentAssertions;

namespace UsersTests.UsersAPI.Controllers.Counters;
[Collection("Tests collection")]
public class UsersCountControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturn200OK()
    {
        // GIVEN
        
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync("/UsersCount");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}