using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace UsersTests.UsersAPI.Controllers.Users.Search;
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