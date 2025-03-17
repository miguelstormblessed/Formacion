using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersTests.UsersManagement.Bookings.Domain;

namespace UsersTests.UsersManagement.Bookings.Application.Search;

public class BookingSearcherByVehicleTest : BookingsModuleApplicationTestCase
{
    private readonly BookingSearcherByVehicle _bookingSearcherByVehicle;

    public BookingSearcherByVehicleTest()
    {
        _bookingSearcherByVehicle = new BookingSearcherByVehicle(this.BookingRepository.Object, this.QueryBus.Object);
    }

    [Fact]
    public async Task ShouldCallGetAllBookingsByVehicle()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        // WHEN
        await _bookingSearcherByVehicle.ExecuteAsync(booking.VehicleResponse.Id);
        // THEN
        this.ShouldHaveCalledGetAllBookingsByVehicleWithCorrectParametersOnce(booking.VehicleResponse.Id);
    }

    [Fact]
    public async Task ShouldCallAskAsyncWithCorrectParameters()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(booking.VehicleResponse.Id);
        // WHEN
        await _bookingSearcherByVehicle.ExecuteAsync(booking.VehicleResponse.Id);
        // THEN
        this.ShouldHaveCalledAskAsyncWithCorrectVehicleFinderQueryParametersOnce(vehicleFinderQuery);
            
    }
}