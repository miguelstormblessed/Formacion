using System.Globalization;
using UsersManagement.Shared.Bookings.Domain.Exceptions;

namespace UsersManagement.Bookings.Domain.ValueObject;

public class BookingDate : IEquatable<BookingDate>
{
    private BookingDate(string date)
    {
        IsValid(date);
        DateValue = date;
    }
    public string DateValue { get; set; }

    public static BookingDate Create(string date)
    {
        return new BookingDate(date);
    }

    private void IsValid(string date)
    {
        DateTime result;
        if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            throw new InvalidDateFormatException();
        }
        if (result.Date < DateTime.Now.Date)
        {
            throw new InvalidDateFormatException();
        }
    }

    public bool Equals(BookingDate? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return DateValue == other.DateValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BookingDate)obj);
    }
    
}