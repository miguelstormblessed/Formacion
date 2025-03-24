using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Shared.Users.Responses;

namespace UsersTests.UsersAPI.Controllers.Bookings.Cancel;

public class BookingCancellerControllerTest : ApiTestCase
{
    [Fact]
    public async Task ShouldReturnBadRequest_WhenIsInvalidId()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString().Replace("-", string.Empty);
        BookingCancellerRequest request = new BookingCancellerRequest(id, false);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PatchAsJsonAsync("/BookingCanceller",request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenBookingNotFound()
    {
        // GIVEN
        BookingCancellerRequest request = new BookingCancellerRequest(Guid.NewGuid().ToString(), false);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PatchAsJsonAsync("/BookingCanceller",request);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturn200Ok()
    {
        // GIVEN
        BookingId bookingId = BookingId.Create("4902524d-f374-4a5d-bf7a-e1b008485e84");
        BookingStatus bookingStatus = BookingStatus.Create(true);
        
        
        BookingCancellerRequest bookingCancellerRequest = new BookingCancellerRequest(bookingId.IdValue, bookingStatus.StatusValue);
        // WHEN
        HttpResponseMessage response = await this.HttpClient.PatchAsJsonAsync("/BookingCanceller",bookingCancellerRequest);
        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}