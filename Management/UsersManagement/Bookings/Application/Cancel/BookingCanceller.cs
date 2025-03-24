using Users.Shared.Bookings.Domain.Exceptions;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Bookings.Infrastructure;

namespace UsersManagement.Bookings.Application.Cancel;

public class BookingCanceller
{
    private readonly IBookingRepository _bookingRepository;
    private readonly BookingFinder _bookingFinder;

    public BookingCanceller(IBookingRepository bookingRepository, BookingFinder bookingFinder)
    {
        _bookingRepository = bookingRepository;
        _bookingFinder = bookingFinder;
    }

    public void Execute(BookingStatus status, BookingId bookingId)
    {
        Booking? booking = _bookingFinder.Execute(bookingId);
        if (booking == null)
        {
            throw new BookingNotFoundException();
        }
        this._bookingRepository.Patch(status, bookingId);
    }
}