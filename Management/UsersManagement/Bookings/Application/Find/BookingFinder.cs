using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;

namespace UsersManagement.Bookings.Application.Find;

public class BookingFinder
{
    public BookingFinder(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    private readonly IBookingRepository _bookingRepository;

    public Booking? Execute(BookingId bookingId)
    {
        Booking? booking = _bookingRepository.GetBookingById(bookingId);
        if (booking == null)
        {
            throw new BookingNotFoundException();
        }
        return booking;
    }
    
}