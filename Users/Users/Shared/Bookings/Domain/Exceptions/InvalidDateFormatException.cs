namespace Users.Shared.Bookings.Domain.Exceptions;

public class InvalidDateFormatException : Exception
{
    public InvalidDateFormatException () : base ("Invalid Date Format. Format must be dd/mm/yyyy"){}
}