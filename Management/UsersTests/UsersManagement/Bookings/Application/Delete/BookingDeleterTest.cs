using UsersManagement.Bookings.Application.Delete;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Users.Domain.Commands;
using UsersTests.UsersManagement.Bookings.Domain;

namespace UsersTests.UsersManagement.Bookings.Application.Delete;

public class BookingDeleterTest : BookingsModuleApplicationTestCase
{
    private readonly BookingDeleter _bookingDeleter;
    private readonly BookingFinder _bookingFinder;
    public BookingDeleterTest()
    {
        this._bookingFinder = new BookingFinder(this.BookingRepository.Object);
        _bookingDeleter = new BookingDeleter(this.BookingRepository.Object,this._bookingFinder, this.CommandBus.Object);
    }

    [Fact]
    public void ShouldCallDeleteWithCorrectParametersOnce()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        this._bookingDeleter.Execute(booking.Id);
        // THEN
        this.ShouldHaveCalledDeleteWithCorrectParametersOnce(booking.Id);
    }

    /*[Fact]
    public void ShouldCallPublishAsyncWithCorrectParametersOnce()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        this._bookingDeleter.Execute(booking.Id);
        // THEN
        this.ShouldHaveCalledPublishAsyncWithCorrectParametersOnce<BookingDeleted>();
    }*/
    [Fact]
    public void ShouldCallDispatchAssyncWithCorrectParametersOnce()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        UserUpdaterCommand command = UserUpdaterCommand.Create(
            booking.UserResponse.Id,
            booking.UserResponse.Name,
            booking.UserResponse.Email,
            booking.UserResponse.State,
            null);
        this.ShouldFindBooking(booking.Id, booking);
        // WHEN
        this._bookingDeleter.Execute(booking.Id);
        // THEN
        this.ShouldHaveCalledDispatchAsyncWithCorrectParametersOnce(command);
    }
}