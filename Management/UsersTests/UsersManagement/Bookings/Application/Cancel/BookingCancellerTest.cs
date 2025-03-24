using FluentAssertions;
using UsersManagement.Bookings.Application.Cancel;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;
using UsersTests.UsersManagement.Bookings.Domain;

namespace UsersTests.UsersManagement.Bookings.Application.Cancel;

public class BookingCancellerTest : BookingsModuleApplicationTestCase
{
    private readonly BookingCanceller _bookingCanceller;
    private readonly BookingFinder _bookingFinder;
    public BookingCancellerTest()
    {
        _bookingFinder = new BookingFinder(this.BookingRepository.Object);
        _bookingCanceller = new BookingCanceller(this.BookingRepository.Object, this._bookingFinder);
    }

    [Fact]
    public void ShouldCallPatchWithCorrectParametersOnce()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        _bookingCanceller.Execute(booking.Status, booking.Id);
        // THEN
        this.ShouldHaveCalledPatchWithCorrectParametersOnce(booking.Status, booking.Id);
    }
    [Fact]
    public void ShouldThrowBookingNotFountException_WhenBookingIsNotFound()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        // WHEN
        var result = () => this._bookingCanceller.Execute(booking.Status, booking.Id);
        // THEN
        result.Should().Throw<BookingNotFoundException>();
    }
}