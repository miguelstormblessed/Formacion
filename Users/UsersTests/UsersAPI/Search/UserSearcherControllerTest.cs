using System.Net;
using FluentAssertions;

namespace UsersTests.UsersAPI.Search;
[Collection("Tests collection")]
public class UserSearcherControllerTest : ApiTestCase
{
    [Fact]
    public async Task GetUserSearcher()
    {
        // GIVEN
        
        // WHEN
        var response = await this.HttpClient.GetAsync("/UserSearcher");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}