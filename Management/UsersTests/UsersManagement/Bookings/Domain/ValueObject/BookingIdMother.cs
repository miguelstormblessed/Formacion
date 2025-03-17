using UsersManagement.Bookings.Domain.ValueObject;

namespace UsersTests.UsersManagement.Bookings.Domain.ValueObject;

public class BookingIdMother
{
    public static BookingId CreateRandom()
    {
        return BookingId.Create(Guid.NewGuid().ToString());
    }
    
}