namespace UsersManagement.Shared.Bookings.Domain.Requests;

public class BookingCancellerRequest
{
    public string BookingId { get; set; }
    public bool Cancel { get; set; }

    public BookingCancellerRequest(string bookingId, bool cancel)
    {
        BookingId = bookingId;
        Cancel = cancel;
    }
}

    