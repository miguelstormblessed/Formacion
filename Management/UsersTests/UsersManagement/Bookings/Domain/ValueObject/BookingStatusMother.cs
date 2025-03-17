using UsersManagement.Bookings.Domain.ValueObject;

namespace UsersTests.UsersManagement.Bookings.Domain.ValueObject;

public class BookingStatusMother
{
    public static BookingStatus CreateRandom()
    {
        Random random = new Random();
        return BookingStatus.Create(random.Next(0,1) == 0);
    }
}