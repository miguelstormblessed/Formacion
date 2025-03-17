using System.Text.RegularExpressions;
using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersManagement.Bookings.Domain.ValueObject;

public class BookingId : IEquatable<BookingId>
{
    private BookingId(string idValue)
    {
        IsValid(idValue);
        IdValue = idValue;
    }
    public string IdValue { get; set; }

    public static BookingId Create(string id)
    {
        return new BookingId(id);
    }

    private void IsValid(string id)
    {
        string uuidPattern = @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";

        if (!Regex.IsMatch(id, uuidPattern))
        {
            throw new InvalidIdException();
        }
    }

    public bool Equals(BookingId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdValue == other.IdValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BookingId)obj);
    }
    
}