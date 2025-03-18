namespace Users.Shared.Bookings.Domain.Exceptions;

public class BookingNotFoundException : Exception
{
    public BookingNotFoundException() : base("Booking not found") { }
}