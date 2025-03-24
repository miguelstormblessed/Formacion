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
        _bookingSearcherByUser = new BookingSearcherByUser(this.BookingRepository.Object, this.QueryBus.Object);
    }

    [Fact]
    public async Task ShouldCallGetAllBookingsByUserId()
    {
        // GIVEN
        UserResponse userResponse = UserResponseMother.CreateRandom();
        string id = Guid.NewGuid().ToString();
        // WHEN
        await _bookingSearcherByUser.ExecuteAsync(id);
        // THEN
        this.ShouldHaveCalledGetAllBookingsByUserIdWithCorrectParametersOnce(id);
    }
    
}