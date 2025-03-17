using System.Net;
using FluentAssertions;

namespace UsersTests.UsersAPI.Controllers.Bookings.Search;
[Collection("Tests collection")]
public class BookingSearcherControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldGetAllBookings()
    {
        // GIVEN
        
        // WHEN
        HttpResponseMessage response = await this.HttpClient.GetAsync("/BookingSearcher");
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}