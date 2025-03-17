using FluentAssertions;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Bookings.Infrastructure;
using UsersManagement.Shared.Bookings.Domain.Exceptions;
using UsersTests.UsersManagement.Bookings.Domain;

namespace UsersTests.UsersManagement.Bookings.Application.Find;

public class BookingFinderTest : BookingsModuleApplicationTestCase
{
    private readonly BookingFinder _bookingFinder;

    public BookingFinderTest()
    {
        _bookingFinder = new BookingFinder(this.BookingRepository.Object);
    }

    [Fact]
    public void ShouldCallGetByIdOnce()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        this._bookingFinder.Execute(booking.Id);
        // THEN
        this.ShouldHaveCalledGetByIdWithCorrectParametersOnce(booking.Id);
    }
    [Fact]
    public void ShouldFindBookings()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        Booking? result = this._bookingFinder.Execute(booking.Id);
        // THEN
        result.Id.Should().Be(booking.Id);
        result.Date.Should().Be(booking.Date);
        result.UserResponse.Should().Be(booking.UserResponse);
        result.VehicleResponse.Should().Be(booking.VehicleResponse);
    }

    [Fact]
    public void ShouldThrowBookingNotFoundException()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        // WHEN
        Func<Booking?> result = () => this._bookingFinder.Execute(booking.Id);
        // THEN
        result.Should().Throw<BookingNotFoundException>();
    }
}