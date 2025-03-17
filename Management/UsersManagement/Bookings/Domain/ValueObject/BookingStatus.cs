namespace UsersManagement.Bookings.Domain.ValueObject;

public class BookingStatus : IEquatable<BookingStatus>
{
    private BookingStatus(bool statusValue)
    {
        StatusValue = statusValue;
    }

    public static BookingStatus Create(bool status = true)
    {
        return new BookingStatus(status);
    }
    
    public bool StatusValue { get; private set; }

    public bool Equals(BookingStatus? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return StatusValue == other.StatusValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BookingStatus)obj);
    }
    
}