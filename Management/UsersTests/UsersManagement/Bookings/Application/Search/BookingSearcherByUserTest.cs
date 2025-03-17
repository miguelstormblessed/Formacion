using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersTests.UsersManagement.Bookings.Domain;

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
        Booking booking = BookingMother.CreateRandom();
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        await _bookingSearcherByUser.ExecuteAsync(booking.Id.IdValue);
        // THEN
        this.ShouldHaveCalledGetAllBookingsByUserIdWithCorrectParametersOnce(booking.Id.IdValue);
    }

    [Fact]
    public async Task ShouldCallAskAsyncWithCorrectParameters()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        UserFinderQuery userFinderQuery = UserFinderQuery.Create(booking.Id.IdValue);
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        await _bookingSearcherByUser.ExecuteAsync(booking.Id.IdValue);
        // THEN
        this.ShouldHaveCalledAskAsyncWithCorrectUserFinderQueryParametersOnce(userFinderQuery);
    }
}