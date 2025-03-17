using UsersManagement.Bookings.Application.Search;

namespace UsersTests.UsersManagement.Bookings.Application.Search;

public class BookingSearcherTest : BookingsModuleApplicationTestCase
{
    private readonly BookingSearcher _bookingSearcher;

    public BookingSearcherTest()
    {
        _bookingSearcher = new BookingSearcher(this.BookingRepository.Object);
    }

    [Fact]
    public async Task ShouldCallGetAllBookingsOnce()
    {
        // GIVEN
        
        // WHEN
        await _bookingSearcher.ExecuteAsync();
        // THEN
        this.ShouldHaveCalledGetAllBookingsWithCorrectParametersOnce();
    }
}