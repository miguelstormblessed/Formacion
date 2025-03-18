using System.Net;
using System.Text.Json.Serialization;
using FluentAssertions;
using Newtonsoft.Json;
using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Shared.Users.Responses;

namespace UsersTests.UsersManagement.Bookings.Application.Search;

public class BookingSearcherByUserTest : BookingsModuleApplicationTestCase
{
    private readonly BookingSearcherByUser _bookingSearcherByUser;

    public BookingSearcherByUserTest()
    {
        _bookingSearcherByUser = new BookingSearcherByUser(this.BookingRepository.Object, this.QueryBus.Object, this.HttpClientService.Object);
    }

    [Fact]
    public async Task ShouldCallGetAllBookingsByUserId()
    {
        // GIVEN
        UserResponse userResponse = UserResponseMother.CreateRandom();
        HttpResponseMessage message = new HttpResponseMessage();
        message.Content = new StringContent(JsonConvert.SerializeObject(userResponse));
        message.StatusCode = HttpStatusCode.OK;
        this.ShouldFindUserByHttp(message);
        string id = Guid.NewGuid().ToString();
        // WHEN
        await _bookingSearcherByUser.ExecuteAsync(id);
        // THEN
        this.ShouldHaveCalledGetAllBookingsByUserIdWithCorrectParametersOnce(id);
    }

    [Fact]
    public async Task ShouldThrowUserNotFoundException()
    {
        // GIVEN
        UserResponse userResponse = UserResponseMother.CreateRandom();
        HttpResponseMessage message = new HttpResponseMessage();
        message.Content = new StringContent(JsonConvert.SerializeObject(userResponse));
        message.StatusCode = HttpStatusCode.NotFound;
        await this.ShouldFindUserByHttp(message);
        string id = Guid.NewGuid().ToString();
        // WHEN
        var result = () =>  _bookingSearcherByUser.ExecuteAsync(id);
        // THEN
        await result.Should().ThrowAsync<UserNotFoundException>();
    }
}